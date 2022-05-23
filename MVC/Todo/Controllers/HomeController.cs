using Microsoft.AspNetCore.Mvc;

namespace Todo.Contollers
{
    [ApiController]
    public class HomeContoller : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public string Get()
        {
            return "João é lindão";
        }
    }
}