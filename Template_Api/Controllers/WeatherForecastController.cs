using Core.Elastic.Abstract;
using Core.LogAkn.Abstract;
using Core.LogAkn.Extantions;
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

        private readonly ILogService _logService;
        private readonly IAknUser _aknUser;
        public WeatherForecastController(IElasticSearchProvider<ElasticSearchTestobject> elasticSearchProvider, ILogService logService, IAknUser aknUser)
        {
            _elasticSearchProvider = elasticSearchProvider;
      
            _logService = logService;
           _aknUser = aknUser;
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
            //_logger.LogInformation("test deneme logu info");
            //_logger.LogError("test deneme logu error");
            //_logger.LogWarning("test deneme logu warning");
            int userID = 0;

            if (_aknUser is AknUser user)
            {
                userID = user.UserId;
            }

            _logService.LogInformation("{0} logu eklendi user Id :{1}","Kadir akın", userID);
            return Summaries;
        }

        [HttpPost]
        public IEnumerable<object> Login([FromBody] TestInputObject test)
        {

            return Summaries;
        }
    }
}
