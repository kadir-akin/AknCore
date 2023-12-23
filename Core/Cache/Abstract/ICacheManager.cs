using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Abstract
{
    public interface ICacheManager
    {
        public void Add(string key, object data);


        public bool Any(string key);



        public T Get<T>(string key);



        public void Remove(string key);



        public Task ClearAsync();
       
    }
}
