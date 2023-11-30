using Core.Elastic.Abstract;
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
            var _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            
            var projectInfoConfiguration = _configuration.GetSection("ProjectInfoConfiguration");

            if (projectInfoConfiguration.Exists())
                Console.WriteLine("implement edilmeli"); //services.Configure<ProjectInfoConfiguration>(projectInfoConfiguration);
            else
                throw new System.Exception("ProjectInfoConfiguration not found");

            var projectInfo = services.BuildServiceProvider().GetService<IOptions<ProjectInfoConfiguration>>();

            if (projectInfo.Value.EnableLog)
            {
                var logConfigurationsection = _configuration.GetSection("LogConfiguration");

                if (logConfigurationsection.Exists())
                    Console.WriteLine("implement edilmeli"); //services.Configure<LogConfiguration>(logConfigurationsection);
                else
                    throw new System.Exception("LogConfiguration not found");
               
            }




            return services;
        }
    }
}
