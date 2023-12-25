using Core.Cache.Abstract;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Concrate
{
    public class CacheManager : ICacheManager
    {
        private readonly ICacheFactory _cacheFactory;
        private readonly IRedisProvider _redisProvider;
        private readonly IMemoryCacheProvider _memoryCacheProvider;
        public CacheManager(ICacheFactory cacheFactory)
        {
            _cacheFactory= cacheFactory;
            _redisProvider =(IRedisProvider)_cacheFactory.GetProvider(CacheTypeEnum.Redis);
            _memoryCacheProvider = (IMemoryCacheProvider)_cacheFactory.GetProvider(CacheTypeEnum.Memory);
        }
        public void Add(string key, object data,TimeSpan? timeSpan)
        {
            _redisProvider.Add(key, data, timeSpan);
        }

        public Task<T> AddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan) where T : class
        {
            return _redisProvider.AddAsync<T>(key, func, timeSpan);
        }

        public Task ClearAsync()
        {
            return _redisProvider.ClearAsync();
        }

        public bool Exist(string key)
        {
            return _redisProvider.Exist(key);
        }

        public T Get<T>(string key)
        {
            return _redisProvider.Get<T>(key);
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            return _redisProvider.GetAsync<T>(key);
        }

        public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T : class
        {
            return _redisProvider.GetOrAddAsync<T>(key, func, timeSpan);    
        }

        public async Task<T> GetOrAddMemoryFirstAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T : class
        {
            var memoryAny =_memoryCacheProvider.Exist(key);
            T result;
           
            if (memoryAny)
                result = _memoryCacheProvider.Get<T>(key);
            else
            {
                result= await _memoryCacheProvider.GetOrAddAsync<T>(key, func, timeSpan);
               
                _redisProvider.GetOrAddAsync<T>(key, func, timeSpan);
                _redisProvider.Publish<T>(key, result, timeSpan);
            }

            return result;

        }

        public void Remove(string key)
        {
           _redisProvider.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            return _redisProvider.RemoveAsync(key);
        }
    }
}
