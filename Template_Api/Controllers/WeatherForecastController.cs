using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template_Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
  
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

      

        public WeatherForecastController()
        {
        }

       

        [HttpPost]
        public IEnumerable<object> abc([FromBody]TestInputObject test)
        {
       
            return Summaries;
        }
    }
}
