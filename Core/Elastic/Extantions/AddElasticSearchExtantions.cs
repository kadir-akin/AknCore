using Core.Elastic.Abstract;
using Core.Elastic.Concrate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Elastic.Extantions
{
    public static class AddElasticSearchExtantions
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services)
        {
                                 
            var _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var elasticConfig = _configuration.GetSection("ElasticSearchConfiguration");
            if (elasticConfig.Exists())
                 services.Configure<ElasticSearchConfiguration>(elasticConfig);
            else
                throw new System.Exception("ElasticSearchConfiguration Not Found");

            services.AddScoped(typeof(IElasticSearchProvider<>), typeof(ElasticSearchProvider<>));

            return services;
        }
    }
}
