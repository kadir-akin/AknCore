using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Template_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        [HttpPost]
        public IEnumerable<object> Get()
        {

            return Summaries;
        }
    }
}
