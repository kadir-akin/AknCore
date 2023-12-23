﻿using Core.Cache.Abstract;
using Core.Utilities;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<bool> ExistAsync(string key) 
        { 
            return _redisServer.Database.KeyExistsAsync(key);
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T: class
        {
            if (await ExistAsync(key))
            {
               return await GetAsync<T>(key);
            }
            else
            {
                var result =await  func();
                await _redisServer.Database.HashSetAsync(key,RedisUtilities.ToHashEntries(result));
                return result;
            }

        }


        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var hashEntryList = await _redisServer.Database.HashGetAllAsync(key);
            return RedisUtilities.HashEntryToObject<T>(hashEntryList);
        }

        
    }
}
