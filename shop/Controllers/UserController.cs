using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Model;

namespace Shop.Controllers
{
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var receiveUsers = await context.Users.AsNoTracking().ToArrayAsync();
                return Ok(receiveUsers);
            }
            catch (Exception)
            {

                return StatusCode(500, "Usuario não encontrado");
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetId(int id, [FromServices] DataContext context,[FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                if (context.Users == null)
                    return StatusCode(500, "Usuario inesxistente");

                var receiveUserId = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return Ok(receiveUserId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Usuario não encontrado");
            }
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] User user,[FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception)
            {

                return StatusCode(500, "Falha ao cadastrar o usuario");
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Post(int id,[FromBody] User user, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var updateUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (updateUser == null)
                    return StatusCode(404, "Usuario não encontrado");

                context.Entry<User>(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception)
            {

                return StatusCode(500, "Falha ao cadastrar o usuario");
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromBody] User user, [FromServices] DataContext context)
        {
            var receiveDeleteUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (receiveDeleteUser == null)
                return StatusCode(404, "Usuario não encontrado");
            try
            {
                context.Users.Remove(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Falha ao excluir um usuario");
            }
        }
    }
}
