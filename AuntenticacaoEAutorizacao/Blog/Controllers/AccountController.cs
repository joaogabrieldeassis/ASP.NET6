using Blog.DataContext;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> Post([FromBody] RegisterViewModel model,[FromServices] BlogDataContext context)
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
            return Ok(new ResultViewModel<dynamic>(new {
            user = user.Email, receivePassword
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
    public IActionResult Login([FromServices] TokenServices tokenServices)
    {
        var token = tokenServices.GenerateToken(null);
        return Ok(token);
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

