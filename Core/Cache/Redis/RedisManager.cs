using Core.Cache.Abstract;
using Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Redis
{
    public class RedisManager :ICacheManager
    {
        private RedisServer _redisServer;

        public RedisManager(RedisServer redisServer)
        {
            _redisServer = redisServer;

        }

        public void Add(string key, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            _redisServer.Database.StringSet(key, jsonData);
        }

        public bool Any(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (Any(key))
            {
                string jsonData = _redisServer.Database.StringGet(key);
                return jsonData.ToObject<T>();
            }

            return default;
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public Task ClearAsync()
        {
           return _redisServer.FlushDatabaseAsync();
        }
    }
}
