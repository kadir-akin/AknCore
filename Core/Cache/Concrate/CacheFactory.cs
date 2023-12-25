using Core.Cache.Abstract;
using Core.Cache.Memory;
using Core.Cache.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Cache.Concrate
{
    public class CacheFactory :ICacheFactory
    {
        public CacheFactory()
        {
            if (Providers == null)
                Providers = new List<ICacheProvider>();
        }
        public List<ICacheProvider> Providers { get; set; }
        public void AddProvider(ICacheProvider cacheProvider) 
        {
            
            if (!Providers.Contains(cacheProvider))
            {
                Providers.Add(cacheProvider);   
            }
        }


        public ICacheProvider GetProvider(CacheTypeEnum cacheType) 
        {         
            switch (cacheType) 
            {
                case CacheTypeEnum.Redis:
                        return Providers.FirstOrDefault(x => x is RedisProvider);
                case CacheTypeEnum.Memory:
                        return Providers.FirstOrDefault(x => x is MemoryCacheProvider);
                default:
                    return Providers.FirstOrDefault(x => x is RedisProvider);

            }
        }


         
    }
}
