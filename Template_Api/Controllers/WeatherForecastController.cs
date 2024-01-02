using Core.Bus.Abstract;
using Core.Cache.Abstract;
using Core.Database.EF.Abstract;
using Core.Elastic.Abstract;
using Core.LogAkn.Abstract;
using Core.LogAkn.Extantions;
using Core.Security.Abstract;
using Core.Security.Filter;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus;
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
        private readonly ILogService _logService;
        private readonly IAknUser _aknUser;
        private readonly IRabbitMqProvider<BusMessageTest> _rabbitMqProvider;
        private readonly ICacheManager _cacheManager;
        private readonly IEfUnitofWork _efUnitOfWork;
        private readonly IMongoExampleRepository _mongoExampleRepository;
        private readonly IMongoCustomerRepository _mongoCustomerRepository;
        public WeatherForecastController( ILogService logService, IAknUser aknUser, IEfUnitofWork efUnitOfWork, IMongoExampleRepository mongoExampleRepository, IMongoCustomerRepository mongoCustomerRepository)
        {     
            _logService = logService;
           _aknUser = aknUser;
            //_cacheManager = cacheManager;
            //_rabbitMqProvider = rabbitMqProvider;
            _efUnitOfWork= efUnitOfWork;
            _mongoExampleRepository = mongoExampleRepository;
            _mongoCustomerRepository = mongoCustomerRepository; 
      
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
            var stringKey = "BusMessage:Deneme:3939";


            var a =_efUnitOfWork.GetRepository<UserEntity>();
          var b = await a.GetAsync();

         var c=   await _efUnitOfWork.ExecuteAsync<UserEntity>(async ()=>
            {

              return await _efUnitOfWork.GetRepository<UserEntity>().AddAsync(new UserEntity() { FirstName = "Kadir Test", LastName = "Kadir Test LAst Name" });
            });


            var resultAddMongo = await _mongoExampleRepository.AddAsync(new MongoExampleCollection()
            {

                FirstName = "Deneme verisi 2",
                Name = "Deneme versii 2",
                Age=15,
                CreateDate = DateTime.UtcNow,  
            });

            var getResultMongo = _mongoExampleRepository.Get()?.ToList();



            return getResultMongo;
            //var result= await _cacheManager.GetOrAddMemoryFirstAsync<BusMessageTest>(stringKey,async ()=>
            //{
            //  // await _rabbitMqProvider.Publish(new BusMessageTest() { Deneme = "Test verisi girrildi  " + Guid.NewGuid().ToString() });
            //   // var logresult= await _rabbitMqProvider.MessageCount();
            //    return new BusMessageTest() 
            //    { 
            //     Deneme="memoryFirsDenemesi"
            //    };
            //},TimeSpan.FromDays(30));


            //return await _cacheManager.GetMemoryFirstAsync<BusMessageTest>(stringKey);
           // var result=  _cacheManager.Get<BusMessageTest>(stringKey);
          
            //var userhttpContext = HttpContext.User;
            //var threadUser = AknUserUtilities.GetCurrentUser();
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
            //int userID = 0;

            //if (_aknUser is AknUser user)
            //{
            //    userID = user.UserId;
            //}

            //_logService.LogInformationAsync("{0} logu eklendi user Id :{1}","Kadir akın", userID);
            //return result;
        }

        [HttpPost]
        public IEnumerable<object> Login([FromBody] TestInputObject test)
        {
           
            return Summaries;
        }
    }
}
