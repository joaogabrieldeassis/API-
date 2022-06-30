using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Model;

namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x=>x.Category).AsNoTracking().ToListAsync();
            return Ok(products);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetbyId(int id,[FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
            return Ok(products);
        }
        [HttpGet]
        [Route("categories/{id:int}")] //
        public async Task<ActionResult> GetId(int id, [FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            return Ok(products);
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context,[FromBody] Product model)
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
    }
}
