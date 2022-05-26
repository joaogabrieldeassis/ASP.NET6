using Blog.DataContext;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //Iniciando o CRUD

        //Read
        [HttpGet("v1/categories")] //v1 = verssão 1 dp meu app
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

        //Read
        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync( [FromRoute] int id, [FromServices] BlogDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x=>x.Id == id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        //Create
        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody]Category model, [FromServices] BlogDataContext context)
        {
            await context.Categories.AddAsync(model); //Esperar salvar
            await context.SaveChangesAsync();//Esperar salvar

            return Created($"v1/categories/{model.Id}",model);
        }


        //Update
        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model, [FromServices] BlogDataContext context)
        {
            var receivePut = await context.Categories.FirstOrDefaultAsync(x=>x.Id == id);   //Esperar Atualizar 
            if (receivePut == null)
                return NotFound();

            receivePut.Name = model.Name;
            receivePut.Slug = model.Slug;

            context.Categories.Update(receivePut);
            await context.SaveChangesAsync();

            return Ok(receivePut);
        }

        //Delete
        [HttpDelete("v1/categories/{id:int}")] 
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromBody] Category model, [FromServices] BlogDataContext context)
        {
            var receiveDelete = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);  //Esperar deletar
            if (receiveDelete == null)
                return NotFound();

            context.Categories.Remove(receiveDelete);
            await context.SaveChangesAsync();

            return Ok(receiveDelete);
        }

    }
}
