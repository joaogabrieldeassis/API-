using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Model;
using Shop.Services;
namespace Shop.Controllers
{
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles ="manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            
            try
            {
                var receiveUsers = await context.Users.AsNoTracking().ToListAsync();
                return Ok(receiveUsers);
            }
            catch (Exception)
            {

                return StatusCode(500, "Usuario não encontrado");
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles ="menager")]
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
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] User user, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                user.Role = "employee";
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                user.Password = "";
                return Ok(user);
            }
            catch (Exception)
            {

                return StatusCode(500, "Falha ao cadastrar o usuario");
            }
        }
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authentication([FromBody] User user,[FromServices] DataContext context)
        {
            
                var receiveUser = await context.Users.AsNoTracking()
                    .Where(x=>x.Username == user.Username && x.Password == user.Password)
                    .FirstOrDefaultAsync();

                if (receiveUser == null)
                    return StatusCode(404, "Usuario ou senha invalidos");

                var token = TokenServices.GenerateToken(receiveUser);
            return new
            {
                
                token = token,
            };
                await context.SaveChangesAsync();
            user.Password = "";
                return Ok(user);
         
            
                return StatusCode(500, "Falha ao cadastrar o usuario");
            
        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles ="manager")]
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
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles ="manager")]
        public async Task<IActionResult> Delete(int id, [FromBody] User user, [FromServices] DataContext context)
        {
            var receiveDeleteUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (receiveDeleteUser == null)
                return StatusCode(404, "Usuario não encontrado");
            try
            {
                context.Users.Remove(user);
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Falha ao excluir um usuario");
            }
        }
    }
}
