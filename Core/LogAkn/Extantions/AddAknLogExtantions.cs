using Core.Elastic.Abstract;
using Core.LogAkn.Abstract;
using Core.LogAkn.Concrate;
using Core.LogAkn.LoggerProvider;
using Core.RequestContext.Concrate;
using Core.Security.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Extantions
{
    public static class AddAknLogExtantions
    {
        public static IServiceCollection AddAknLogDependency(this IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            var servisProvider = services.BuildServiceProvider();
            var _configuration = servisProvider.GetService<IConfiguration>();

            var logConfigurationsection = _configuration.GetSection("LogConfiguration");
            var logInfo = servisProvider.GetService<IOptions<LogConfiguration>>();

            if (logConfigurationsection.Exists())
                Console.WriteLine("implement edilmeli"); //services.Configure<LogConfiguration>(logConfigurationsection);
            else
                throw new System.Exception("LogConfiguration not found");

            if (logInfo.Value.EnableLog)
            {
                if (logInfo.Value.EnableElasticLogProvider)
                {
                    services.AddScoped<IElasticLoggerProvider, ElasticLoggerProvider>();
                }

                if (logInfo.Value.EnableDebugLogProvider)
                {
                    services.AddScoped<IDebugLoggerProvider, DebugLoggerProvider>();
                }

            }

            return services;
        }
    }
}
