using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Abstract
{
    public interface ICacheManager
    {
        public void Add(string key, object data, TimeSpan? timeSpan);
        public bool Exist(string key);
        public T Get<T>(string key);
        public void Remove(string key);
        public Task RemoveAsync(string key);
        public Task ClearAsync();
        public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T : class;
        public Task<T> GetAsync<T>(string key) where T : class;
        public Task<T> AddAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan) where T : class;

        public Task<T> GetOrAddMemoryFirstAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeSpan = null) where T : class;
    }
}
