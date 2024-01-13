using Core.Database.Mongo.Abstract;
using Core.Database.Mongo.Concrate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.Extantions
{
    public static class AddMongoExtantions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services,Action<IServiceCollection> action =null) 
        {

            var _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var mongoSection = _configuration.GetSection("MongoConfiguration");
            if (!mongoSection.Exists())
                throw new System.Exception("MongoConfiguration Not Found");

            services.Configure<MongoConfiguration>(mongoSection);

            var mongoConfig = services.BuildServiceProvider().GetService<IOptions<MongoConfiguration>>();
                        
            services.AddTransient<IMongoClient>(sp => 
            {
                var client= new MongoClient($"mongodb://{mongoConfig.Value.UserName}:{mongoConfig.Value.Password}@{mongoConfig.Value.Server}?retryWrites=false");           
                return client;
            });
          
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
           
            if (action != null)
            {
                action.Invoke(services);
            }

            return services;
        }

    }
}
