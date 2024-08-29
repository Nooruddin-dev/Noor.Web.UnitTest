
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noor.Web.Api.Test.Models;

namespace Noor.Web.Api.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Users> result = new List<Users>
            {
                new Users {Id=1, Name = "Noor"},
                new Users {Id=2, Name = "test"},
                new Users {Id=3, Name = "test 2"},
            };
       

            return Ok(result);
        }
    }
}
