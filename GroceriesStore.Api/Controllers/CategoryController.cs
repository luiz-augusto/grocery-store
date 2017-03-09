using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using GroceriesStore.Domain.Enums;
using System.Linq;

namespace GroceriesStore.Api.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController()
        {

        }

        [HttpGet]
        [Route("v1/categories")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = from Category d in Enum.GetValues(typeof(Category))
                               select new { ID = (int)d, Name = d.ToString() };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex });
            }
        }
    }
}
