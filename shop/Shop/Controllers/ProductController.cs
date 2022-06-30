using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController:ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x=>x.Category).AsNoTracking().ToListAsync();
            return Ok(products);
        }
        [HttpGet]
        [Route("id:int")]
        public async Task<IActionResult> GetId(int id,[FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
            return Ok(products);
        }
    }
}
