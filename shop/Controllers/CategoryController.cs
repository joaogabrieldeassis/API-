using Microsoft.AspNetCore.Mvc;
using shop.Model;

namespace shop.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            
            return "GET = Red (Pegar as informações)";
        }
        [HttpGet]
        [Route("{id:int}")]
        public string Getid(int id)
        {

            return "GET = Red (Pegar as informações)";
        }
        [HttpPost]
        [Route("")]
        public Category Post([FromBody]Category category)
        {
            return category;
        }
        
        [HttpPut]
        [Route("{id:int}")]
        public Category Put(int id,[FromBody] Category model)
        {
            if (model.Id == id)
            {
                return model;
            } 
            return null;
        }
        [Route("")]
        [HttpDelete]
        public string Delete()
        {
            return "Delete = Delete (Delete)";
        }
    }
}
