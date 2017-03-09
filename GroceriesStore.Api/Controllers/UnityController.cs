using Microsoft.AspNetCore.Mvc;
using GroceriesStore.Domain.Repositories;
using System.Threading.Tasks;
using System;
using GroceriesStore.Domain.Enums;
using System.Linq;

namespace GroceriesStore.Api.Controllers
{
    public class UnityController : Controller
    {
        private readonly IGroceriesRepository _repository;

        public UnityController()
        {

        }

        [HttpGet]
        [Route("v1/unities")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = from Unity d in Enum.GetValues(typeof(Unity))
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
