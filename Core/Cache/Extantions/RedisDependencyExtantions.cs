using Core.Cache.Abstract;
using Core.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache.Extantions
{
    public static class RedisDependencyExtantions
    {
        public static IServiceCollection AddRedis( this IServiceCollection services) 
        {
            var configuration = (IConfiguration)services.BuildServiceProvider().GetService(typeof(IConfiguration));

            var redisSection = configuration.GetSection("RedisConfiguration");

            if (!redisSection.Exists())
                throw new System.Exception("RedisConfiguration not implementant");


            services.Configure<RedisConfiguration>(redisSection);
            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheManager,RedisManager>();
            return services;
        }
    }
}
