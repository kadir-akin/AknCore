using Core.Cache.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache.Abstract
{
    public interface ICacheFactory
    {
        public List<ICacheProvider> Providers { get; set; }
        public void AddProvider(ICacheProvider cacheProvider);

        public ICacheProvider GetProvider(CacheTypeEnum cacheType);
        
    }
}
