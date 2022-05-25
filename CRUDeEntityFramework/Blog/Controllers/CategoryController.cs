using Blog.DataContext;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("categories")]
        public IActionResult Get([FromServices] BlogDataContext context)
        {
            var categories = Ok(context.Categories.ToList());
            return Ok(categories);
        }
        [HttpPost("categories")]
        public IActionResult Post([FromBody] Category category, [FromServices] BlogDataContext context)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return Created($"/{category.Id}",category);
        }
    }
}
