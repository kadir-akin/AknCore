using Core.Elastic.Abstract;
using Core.Security.Abstract;
using Core.Security.Filter;
using Core.Utilities;
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
        private readonly IElasticSearchProvider<ElasticSearchTestobject> _elasticSearchProvider;

        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(IElasticSearchProvider<ElasticSearchTestobject> elasticSearchProvider, ILogger<WeatherForecastController> logger)
        {
            _elasticSearchProvider = elasticSearchProvider;
            _logger = logger;
        }


        [HttpPost]
        public IEnumerable<object> a(TestInputV2 testInputV2)
        {

            return Summaries;
        }
        [HttpPost]
        [AknAuthorizationFilter("TESTROLE")]
        public async Task<object> abc([FromBody] TestInputObject test)
        {
            var userhttpContext = HttpContext.User;
            var threadUser = AknUserUtilities.GetCurrentUser();
            //await _elasticSearchProvider.ChekIndex();
            //var id = Guid.NewGuid().ToString();
            //await _elasticSearchProvider.InsertDocument(new ElasticSearchTestobject()
            //{
            //    Code = "500",
            //    Id = id,
            //    Message = "test message"
            //});
            //var index = await _elasticSearchProvider.GetDocument(id);
            _logger.LogInformation("");
            return Summaries;
        }

        [HttpPost]
        public IEnumerable<object> Login([FromBody] TestInputObject test)
        {

            return Summaries;
        }
    }
}
