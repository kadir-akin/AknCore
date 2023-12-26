using Core.Cache.Abstract;
using Core.Cache.Concrate;
using Core.RequestContext.Concrate;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;

namespace Core.Cache.Redis
{
    public class RedisServer
    {
        private readonly IOptions<RedisConfiguration> _redisConfiguration;
        private readonly IOptions<ProjectInfoConfiguration> _projectInfoConfiguration;
        private readonly ICacheFactory _cacheFactory;
        private readonly IMemoryCacheProvider _memoryCacheProvider;
        public RedisServer(IOptions<RedisConfiguration> redisConfiguration, IOptions<ProjectInfoConfiguration> projectInfoConfiguration, ICacheFactory cacheFactory)
        {
            _redisConfiguration = redisConfiguration;
            _projectInfoConfiguration = projectInfoConfiguration;
            _cacheFactory = cacheFactory;
            CurrentDatabaseId = _redisConfiguration.Value.Database;
            ConnectionString = _redisConfiguration.Value.Host;
            ConnectionMultiplexer = CreateConnection();
            Database = ConnectionMultiplexer.GetDatabase(CurrentDatabaseId);
            Subscriber=ConnectionMultiplexer.GetSubscriber();
            //Channel = $"{_projectInfoConfiguration.Value.ProjectName}_{_projectInfoConfiguration.Value.ApplicationName}_Akn_Redis_Channel";
            Channel = "ChannelDeneme";
            ClientName = ConnectionMultiplexer.ClientName;
            _cacheFactory.RedisClientName = ClientName;

            if (_redisConfiguration.Value.MemoryFirst)
            {               
                _memoryCacheProvider = (IMemoryCacheProvider)_cacheFactory.GetProvider(Concrate.CacheTypeEnum.Memory);
                Subscribe();
            }
        }
        public ConnectionMultiplexer ConnectionMultiplexer { get; set; }
        public IDatabase Database { get; set; }
        public ISubscriber Subscriber { get; set; }
        public int CurrentDatabaseId { get; set; }
        public string ConnectionString { get; set; }
        public string Channel { get; set; }
        public string ClientName { get; set; }

        public async Task FlushDatabaseAsync()
        {
            foreach (var item in ConnectionMultiplexer.GetServers())
            {
               await  item.FlushDatabaseAsync(CurrentDatabaseId);
            }
                       
        }

        public Task<long> Publish<T>(string key,T value,TimeSpan? timeSpan) where T:class 
        {
            var cacheEntry = new CacheEntry(ClientName,key, value, timeSpan);
            return Subscriber.PublishAsync(Channel, JsonConvert.SerializeObject(cacheEntry),CommandFlags.FireAndForget);

        }

        public async Task Subscribe() 
        {            
             await  Subscriber.SubscribeAsync(Channel, (channel, message) => {
                 
                 string _message = String.Empty;
                 _message = message;               
                 var cacheEntry = _message.ToObject<CacheEntry>();              
                 
                 if (cacheEntry.ClientName != ClientName) 
                 {

                     if (_memoryCacheProvider.Exist(cacheEntry.Key))
                         _memoryCacheProvider.Remove(cacheEntry.Key);

                     _memoryCacheProvider.Add(cacheEntry.Key, JsonConvert.DeserializeObject(cacheEntry.Value), TimeSpan.FromMinutes(cacheEntry.ExpirationTotalMinutes));


                 }                    

             });
           

        }
        private ConnectionMultiplexer CreateConnection()
        {            
            string password = _redisConfiguration.Value.Password;
            bool allowAdmin = _redisConfiguration.Value.AllowAdmin;            
            var options =ConfigurationOptions.Parse(ConnectionString);                 
            options.Password = password;
            options.DefaultDatabase= _redisConfiguration.Value.Database;
            options.AllowAdmin = allowAdmin;
            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(options);            
            return multiplexer;
        }

       

    }
}
