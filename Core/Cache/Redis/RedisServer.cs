using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Redis
{
    public class RedisServer
    {
        private readonly IOptions<RedisConfiguration> _redisConfiguration;
        public RedisServer(IOptions<RedisConfiguration> redisConfiguration)
        {
            _redisConfiguration = redisConfiguration;
            CurrentDatabaseId = _redisConfiguration.Value.Database;
            ConnectionString = _redisConfiguration.Value.Host;
            ConnectionMultiplexer = CreateConnection();
            Database = ConnectionMultiplexer.GetDatabase(CurrentDatabaseId);
            Subscriber=ConnectionMultiplexer.GetSubscriber();
            Channel = "ChannelDeneme";
        }
        public ConnectionMultiplexer ConnectionMultiplexer { get; set; }
        public IDatabase Database { get; set; }
        public ISubscriber Subscriber { get; set; }
        public int CurrentDatabaseId { get; set; }
        public string ConnectionString { get; set; }
        public string Channel { get; set; }


        public async Task FlushDatabaseAsync()
        {
            foreach (var item in ConnectionMultiplexer.GetServers())
            {
               await  item.FlushDatabaseAsync(CurrentDatabaseId);
            }
                       
        }

        public Task<long> Publish<T>(T value) where T:class 
        {
           return Subscriber.PublishAsync(Channel, JsonConvert.SerializeObject(value),CommandFlags.FireAndForget);

        }

        public async Task<T> Subscribe<T>() where T : class
        {
             string _message = String.Empty;
             await  Subscriber.SubscribeAsync(Channel, (channel, message) => { 
                _message = message;
             
             }, CommandFlags.FireAndForget);

            if (!string.IsNullOrEmpty( _message))
            {
                return JsonConvert.DeserializeObject<T>(_message);
            }

            return default(T);

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
