using Blog.DataContext;
using Blog.Services;
using Blog.ViewModel;
using Microsoft.AspNetCore.Mvc;
namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> Post([FromBody] RegisterViewModel model,[FromServices] BlogDataContext context)
    {

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

