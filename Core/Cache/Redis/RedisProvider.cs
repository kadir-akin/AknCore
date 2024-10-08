﻿using Core.Cache.Abstract;
using Core.Cache.Concrate;
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
    public class RedisProvider : IRedisProvider
    {
        private RedisServer _redisServer;

        public RedisProvider(RedisServer redisServer)
        {
            _redisServer = redisServer;

        }

        public void Add(string key, object data,TimeSpan? expire)
        {         
            var hashEntrys = RedisUtilities.ToHashEntries(_redisServer.ClientName,key, data, expire);
            _redisServer.Database.KeyExpire(key, expire);
            _redisServer.Database.HashSet(key, hashEntrys);
        }

        public bool Exist(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (Exist(key))
            {
                var hashEntryList = _redisServer.Database.HashGetAll(key);
               return RedisUtilities.ToObjectCacheEntryHashEntrys<T>(hashEntryList);
               
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
        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan) where T : class
        {
            if (await ExistAsync(key))
            {
                return await GetAsync<T>(key);
            }
            else
            {
                var result = await func();
                var hashEntrys = RedisUtilities.ToHashEntries(_redisServer.ClientName, key, result, timeSpan);                
                await _redisServer.Database.HashSetAsync(key, hashEntrys);
                await _redisServer.Database.KeyExpireAsync(key, timeSpan);
                //var cacheEntry = RedisUtilities.HashEntryToObject<CacheEntry>(hashEntrys);
                //await _redisServer.Publish<CacheEntry>(cacheEntry);
                return result;
            }

        }

        public async Task<T> AddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan) where T : class
        {
            var result = await func();
            var hashEntrys = RedisUtilities.ToHashEntries(_redisServer.ClientName, key, result, timeSpan);           
            await _redisServer.Database.HashSetAsync(key, hashEntrys);
            await _redisServer.Database.KeyExpireAsync(key, timeSpan);
            return result;
        } 
        public Task<bool> ExistAsync(string key) 
        { 
            return _redisServer.Database.KeyExistsAsync(key);
        }
        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var hashEntryList = await _redisServer.Database.HashGetAllAsync(key);
            return RedisUtilities.ToObjectCacheEntryHashEntrys<T>(hashEntryList);
        }
        public Task RemoveAsync(string key) 
        {
          return _redisServer.Database.KeyDeleteAsync(key);
        }

        public Task<long> Publish<T>(string key, T value, TimeSpan? timeSpan) where T : class
        {
          return _redisServer.Publish<T>(key, value, timeSpan);   
        }
    }
}
