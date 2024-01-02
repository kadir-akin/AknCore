using Core.Database.Mongo.Abstract;
using Core.Database.Mongo.Concrate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

            if (action !=null)
            {
                action.Invoke(services);
            }
           
            if (mongoConfig.Value.UseUnitOfWork)
            {
                //services.AddScoped(typeof(IEfUnitofWork), typeof(EfUnitOfWork<TContext>));
            }

            return services;
        }

    }
}
