using Microsoft.Extensions.Options;
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
        }
        public ConnectionMultiplexer ConnectionMultiplexer { get; set; }
        public IDatabase Database { get; set; }
        public int CurrentDatabaseId { get; set; }
        public string ConnectionString { get; set; }
        public async Task FlushDatabaseAsync()
        {
            foreach (var item in ConnectionMultiplexer.GetServers())
            {
               await  item.FlushDatabaseAsync(CurrentDatabaseId);
            }
                       
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
