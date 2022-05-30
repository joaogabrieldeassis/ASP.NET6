using Blog.DataContext;
using Blog.Models;
using Blog.ViewModel;
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
            //Vai retornar os dados do usuario da minha classe ResultViewModel
            return Ok(new ResultViewModel<List<Category>>(categories));
        }

        //Read
        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync( [FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            //Esse catch vai vim do minha classe ResultViewModel do metodo de adicioanar erro
            catch (Exception ex)
            {

                return StatusCode(500, new ResultViewModel<List<Category>>("47D-5 Usuario não encontrado"));
            }
           
        }

        //Create
        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var category = new Category()
                {
                    Name=model.Name,
                    Slug=model.Slug.ToLower(),
                };
                await context.Categories.AddAsync(category); //Esperar salvar
                await context.SaveChangesAsync();//Esperar salvar

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "X12-Y Falha ao tentar inserir dados na categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha ao inserir dados");
            }
        }


        //Update
        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            try
            {
                var receivePut = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);   //Esperar Atualizar 
                if (receivePut == null)
                    return NotFound();

                receivePut.Name = model.Name;
                receivePut.Slug = model.Slug;

                context.Categories.Update(receivePut);
                await context.SaveChangesAsync();

                return Ok(receivePut);
            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, "X1908-4 Falha ao atualizar a tabela ");
            }
            catch(Exception ex)
            {
                return StatusCode(500, "X1454-1 Falha ao Atualizar");
            }
        }

        //Delete
        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var receiveDelete = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);  //Esperar deletar
                if (receiveDelete == null)
                    return NotFound();

                context.Categories.Remove(receiveDelete);
                await context.SaveChangesAsync();

                return Ok(receiveDelete);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(415, "SWE12-X Falaha ao escluir um usuario, verifique o Id inserido");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "D45T-F Falaha ao escluir um usuario ");
            }
        }

    }
}
