using Core.Cache.Abstract;
using Core.Cache.Concrate;
using Core.Cache.Redis;
using Core.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Memory
{
    public class MemoryCacheProvider :IMemoryCacheProvider
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICacheFactory _cacheFactory;
        public MemoryCacheProvider(IMemoryCache memoryCache, ICacheFactory cacheFactory)
        {
            _memoryCache= memoryCache;
            _cacheFactory = cacheFactory;
        }

        public void Add(string key, object data,TimeSpan? timeSpan)
        {
               
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.Add(timeSpan ?? TimeSpan.FromDays(1)))
            };
            _memoryCache.Set(key, new CacheEntry(_cacheFactory.RedisClientName, key, data, timeSpan), cacheExpOptions);
        }

        public async Task<T> AddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan) where T : class
        {
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                 AbsoluteExpiration = new DateTimeOffset(DateTime.Now.Add(timeSpan ?? TimeSpan.FromDays(1)))
            };
             var data = await func();
            _memoryCache.Set(key, new CacheEntry(_cacheFactory.RedisClientName, key,data,timeSpan),cacheExpOptions);
            return data;
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public bool Exist(string key)
        {
            object result;
            bool isThere= _memoryCache.TryGetValue(key, out result);
            return isThere;
        }

        public T Get<T>(string key)
        {
            var result= _memoryCache.Get<CacheEntry>(key);

            if (result ==null)
            {
                return default(T);
            }
            return result.GetData<T>();
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            var result = _memoryCache.Get<CacheEntry>(key);

            if (result == null)
            {
                return Task.FromResult(default(T));
            }
            return Task.FromResult(result.GetData<T>());
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T : class
        {

           var result=  await _memoryCache.GetOrCreateAsync<CacheEntry>(key, (async  y =>
            {
                var resultModel= await func();
                y.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.Add(timeSpan ?? TimeSpan.FromDays(1)));
                return new CacheEntry(_cacheFactory.RedisClientName, key,resultModel,timeSpan);
            }));

            return result?.GetData<T>();          
        }

        public void Remove(string key)
        {
           _memoryCache.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
