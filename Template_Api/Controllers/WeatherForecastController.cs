using Core.Bus.Abstract;
using Core.Cache.Abstract;
using Core.Database.EF.Abstract;
using Core.Database.Mongo.Abstract;
using Core.Database.Mongo.Concrate;
using Core.Elastic.Abstract;
using Core.Elastic.Concrate;
using Core.Elastic.Helper;
using Core.HttpClient.Abstract;
using Core.HttpClient.Concrate;
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
        private readonly IMongoUnitOfWork _mongoUnitOfWork;
        private readonly IElasticSearchProvider<ElasticSearchTestobject> _elasticSearchProvider;
        private readonly IAknHttpClient<TestInternalServiceConfiguration> _testInternalService;
        public WeatherForecastController(IAknHttpClient<TestInternalServiceConfiguration> testInternalService)
        {
            _testInternalService = testInternalService;
            //_logService = logService;
            // _aknUser = aknUser;
            //_cacheManager = cacheManager;
            //_rabbitMqProvider = rabbitMqProvider;
            //_efUnitOfWork= efUnitOfWork;
            // _mongoExampleRepository = mongoExampleRepository;
            //_mongoUnitOfWork = mongoUnitOfWork; 
           // _elasticSearchProvider = elasticSearchProvider;

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

           var result= await _testInternalService.SendAsync<List<TestInternalServiceResponse>>("/posts", HttpMethodType.GET);

            return result;
            //var list = new List<ElasticSearchTestobject>();
            //for (int i = 0; i < 10; i++)
            //{
            //    var testobject = new ElasticSearchTestobject()
            //    {
            //        Code = "code " + i,
            //        CreateDate = DateTime.Now,
            //        Id = Guid.NewGuid().ToString(),
            //        Message = "message + " + i,
            //        Quantity = i
            //    };

            //    list.Add(testobject);
            //}


            //await _elasticSearchProvider.InsertDocument(new ElasticSearchTestobject()
            //{
            //    Code = "Apple Iphone 15 S plus",
            //    CreateDate = DateTime.Now,
            //    Id = Guid.NewGuid().ToString(),
            //    Message = "Apple Iphone 15 S plus",
            //    Quantity = 15,
            //    SuggestOutput = "Apple Iphone 15 S plus",
            //    Suggest = ElasticSearchHelper.ConvertCompletionField(new List<string>
            //     {
            //      "Apple","Iphone","15s","15"
            //     })
            //});
            //await _elasticSearchProvider.InsertDocument(new ElasticSearchTestobject()
            //{
            //    Code = "Galixy 13 plus",
            //    CreateDate = DateTime.Now,
            //    Id = Guid.NewGuid().ToString(),
            //    Message = "Galixy 13 plus",
            //    SuggestOutput = "Galixy 13 plus",
            //    Quantity = 15,
            //    Suggest = ElasticSearchHelper.ConvertCompletionField(new List<string>
            //     {
            //      "Galaxy","Galixy 13 plus","Galaxy 13","13"
            //     })
            //});

            //await _elasticSearchProvider.InsertDocument(new ElasticSearchTestobject()
            //{
            //    Code = "Apple Iphone 13 S plus",
            //    CreateDate = DateTime.Now,
            //    Id = Guid.NewGuid().ToString(),
            //    Message = "Apple Iphone 13 S plus",
            //    SuggestOutput = "Apple Iphone 13 S plus",
            //    Quantity = 15,
            //    Suggest = ElasticSearchHelper.ConvertCompletionField(new List<string>
            //     {
            //      "Apple","Iphone","13s","13"
            //     })
            //});

            //await _elasticSearchProvider.InsertDocuments(list);

            //var elasticSearchBuilder = new ElasticSearchBuilder();
            //elasticSearchBuilder.SetFrom(0)
            //                    .SetSize(11)
            //                    .AddRangeFilter(4, 10, "quantity")
            //                    .AddTermQuery("25", "code");



            //var searchResult = await _elasticSearchProvider.SearchAsync(elasticSearchBuilder);


            //var suggestResult = await _elasticSearchProvider.SuggestAsync(test.Suggest, 10);

            //Dictionary<string, object> result = new Dictionary<string, object>();


            //result.Add("searchResult", searchResult);
            //result.Add("suggestResult", suggestResult);
            //return result;


            //var stringKey = "BusMessage:Deneme:3939";

            //var mongoUnitof = await _mongoUnitOfWork.ExecuteAsync<List<MongoExampleCollection>>(async (x) =>
            // {
            //     var exampleRepository = x.GetRepository<MongoExampleCollection>();

            //     await exampleRepository.AddAsync(new MongoExampleCollection()
            //     {
            //         FirstName = "Galatasaray 2"

            //     });
            //     if (test.Name == "kadir akın1")
            //     {
            //         int b1 = 0;
            //         int b2 = 3;
            //         int b3 = b2 / b1;
            //     }

            //     return exampleRepository.Get()?.ToList();

            // });


            //return mongoUnitof;
            //   var a =_efUnitOfWork.GetRepository<UserEntity>();
            // var b = await a.GetAsync();

            //var c=   await _efUnitOfWork.ExecuteAsync<UserEntity>(async ()=>
            //   {

            //     return await _efUnitOfWork.GetRepository<UserEntity>().AddAsync(new UserEntity() { FirstName = "Kadir Test", LastName = "Kadir Test LAst Name" });
            //   });


            //var resultAddMongo = await _mongoExampleRepository.AddAsync(new MongoExampleCollection()
            //{

            //    FirstName = "Deneme verisi 2",
            //    Name = "Deneme versii 2",
            //    Age = 15,
            //    CreateDate = DateTime.UtcNow,
            //});

            //var getResultMongo = _mongoExampleRepository.Get()?.ToList();
            //return getResultMongo;


            //return getResultMongo;
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
            return Summaries;
        }

        [HttpPost]
        [AllowAuthentication]
        public IEnumerable<object> Product([FromBody] TestInputObject test)
        {

            return Summaries;
        }
    }
}
