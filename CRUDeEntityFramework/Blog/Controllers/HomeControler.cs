using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeControler : ControllerBase
    {
        [HttpGet("")]
       
        public IActionResult GET()
        {
            return Ok();
        }
    }
}
