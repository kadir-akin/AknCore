using Core.Cache.Abstract;
using Core.Cache.Concrate;
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

        public void Add(string key, object data)
        {
            _memoryCache.Set(key, data);
        }

        public async Task<T> AddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan) where T : class
        {
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes((timeSpan?.TotalMinutes ?? 0)              
            };
             var data = await func();
            _memoryCache.Set(key, data,cacheExpOptions);
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
          var result= _memoryCache.Get(key);

            if (result ==null)
            {
                return default(T);
            }

            return (T)result;
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T : class
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
