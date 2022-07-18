using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Model;

namespace Shop.Controllers
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            return Ok(products);
        }
        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetbyId(int id, [FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(products);
        }
        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        //
        public async Task<ActionResult> GetId(int id, [FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            return Ok(products);
        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product model)
        {
            try
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {

                return BadRequest(ModelState);
            }
        }
        //[HttpPut]
        //[Route("{id:int}")]
        // public async Task<IActionResult> Put(int id, [FromServices] DataContext context, [FromBody] Product model)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest();

        //     if (model.Id != id)
        //         return StatusCode(404, "Produto não encontrado");
        //     try
        //     {

        //     }
        //     catch (System.Exception)
        //     {

        //         throw;
        //     }
        // }
    }
}
