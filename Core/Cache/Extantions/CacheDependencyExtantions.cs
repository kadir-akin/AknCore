using Core.Cache.Abstract;
using Core.Cache.Concrate;
using Core.Cache.Memory;
using Core.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache.Extantions
{
    public static class CacheDependencyExtantions
    {
        public static IServiceCollection AddAknCache( this IServiceCollection services) 
        {
            var configuration = (IConfiguration)services.BuildServiceProvider().GetService(typeof(IConfiguration));

            var redisSection = configuration.GetSection("RedisConfiguration");

            if (!redisSection.Exists())
                throw new System.Exception("RedisConfiguration not implementant");


            services.Configure<RedisConfiguration>(redisSection);
            services.AddSingleton<RedisServer>();
            services.AddSingleton<IRedisProvider, RedisProvider>();
            services.AddSingleton(typeof(ICacheFactory),new CacheFactory());

            var cacheFactory =(ICacheFactory) services.BuildServiceProvider().GetService(typeof(ICacheFactory));
            var redisProvider = (IRedisProvider)services.BuildServiceProvider().GetService(typeof(IRedisProvider));
            var redisConfiguration =services.BuildServiceProvider().GetService<IOptions<RedisConfiguration>>();
            cacheFactory.AddProvider(redisProvider);

            if (redisConfiguration.Value.MemoryFirst)
            {
                services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
                var memoryProvider = services.BuildServiceProvider().GetService<IMemoryCacheProvider>();
                cacheFactory.AddProvider(memoryProvider);
            }

            services.AddSingleton<ICacheManager, CacheManager>();
            return services;
        }
    }
}
