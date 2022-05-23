using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/joao")]
        public List<TodoModel> Get([FromServices] AppDbContext context)
        {

            return context.Todos.ToList();
        }
    }
}