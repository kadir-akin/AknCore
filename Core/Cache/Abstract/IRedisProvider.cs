using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Abstract
{
    public interface IRedisProvider :ICacheProvider
    {
        public void Add(string key, object data,TimeSpan? expire);
        public Task<T> AddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan) where T : class;
        public bool Exist(string key);
        public Task<bool> ExistAsync(string key);
        public T Get<T>(string key);
        public Task<T> GetAsync<T>(string key) where T : class;
        public void Remove(string key);
        public Task RemoveAsync(string key);
        public Task ClearAsync();
        public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T : class;
        public Task<long> Publish<T>(string key, T value, TimeSpan? timeSpan) where T : class;
    }
}
