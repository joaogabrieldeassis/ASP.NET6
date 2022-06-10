using Blog.DataContext;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModel;
using Blog.ViewModel.Acoounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> Post([FromBody] RegisterViewModel model, [FromServices] EmailServices email, [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-")
        };
        var receivePassword = PasswordGenerator.Generate(25,includeSpecialChars:true,upperCase:true);
        user.PassWordHash = PasswordHasher.Hash(receivePassword);
        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            
            email.Send(
                user.Name,
                user.Email,
                "Seja bem vindo a loja peixe imports",
                $"Sua senha é {receivePassword}"
                );

            return Ok(new ResultViewModel<dynamic>(new
            {
            user = user.Email
            }));
        }
        catch (DbUpdateException )
        {
            return StatusCode(400, new ResultViewModel<string>("456_X - Este Email já esta cadastrado"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("456-D Falha interna ao cadastrar o usuario"));
        }
    }

    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModels model, [FromServices] BlogDataContext context, [FromServices] TokenServices tokenServices)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var user = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        //Verificando se o usuario existe
            if (user == null)
            return StatusCode(401, new ResultViewModel<string>("Usuario ou senha invalidor"));

        //Verificando se a senha é valida
        if (!PasswordHasher.Verify(user.PassWordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuario ou senha invalidos"));

        try
        {
            var token = tokenServices.GenerateToken(user);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch 
        {

            return StatusCode(500, new ResultViewModel<string>("95qR - Falha interna no servidor"));
        }
    }


}

    /*[Authorize(Roles = "user")]
    [HttpGet("v1/user")]
        public IActionResult GetUser() => Ok(User.Identity.Name);

    [Authorize(Roles = "author")]
    [HttpGet("v1/author")]
        public IActionResult GetAuthor() => Ok(User.Identity.Name);

    [Authorize(Roles = "admin")]
    [HttpGet("v1/admin")]
        public IActionResult GetAdmin() =>Ok(User.Identity.Name);
    }*/

