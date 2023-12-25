using Core.Cache.Abstract;
using Core.Cache.Concrate;
using Core.Utilities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Memory
{
    public class MemoryCacheProvider :IMemoryCacheProvider
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache= memoryCache;
        }

        public void Add(string key, object data,TimeSpan? timeSpan)
        {
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now, timeSpan ?? TimeSpan.MaxValue)
            };
            _memoryCache.Set(key, new CacheEntry(key, data, timeSpan), cacheExpOptions);
        }

        public async Task<T> AddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan) where T : class
        {
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                 AbsoluteExpiration = new DateTimeOffset(DateTime.Now,timeSpan ?? TimeSpan.MaxValue)
            };
             var data = await func();
            _memoryCache.Set(key, new CacheEntry(key,data,timeSpan),cacheExpOptions);
            return data;
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public bool Exist(string key)
        {
          var result= _memoryCache.Get(key);

            if(result==null)    
                return false;

            return true;
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

        public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T : class
        {
            //_memoryCache.GetOrCreateAsync<CacheEntry>(key, (x, y) =>
            //{

            //    return default(CacheEntry);
            //});
            throw new NotImplementedException();
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
