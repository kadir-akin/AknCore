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
            var _loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
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

                var logConfig = services.BuildServiceProvider().GetService<IOptions<LogConfiguration>>();
                var serviceprovider = services.BuildServiceProvider();
                var _hostingEnvironment = serviceprovider.GetService<IHostingEnvironment>();
                var _httpContextAccessor = serviceprovider.GetService<IHttpContextAccessor>();
                var _requestContext = serviceprovider.GetService<IAknRequestContext>();
                var _user = serviceprovider.GetService<IAknUser>();
                
                if (logConfig.Value.EnableElasticLogProvider)
                {
                    var _elasticSearchprovider = serviceprovider.GetService<IElasticSearchProvider<RequestContextLog>>();
                    _loggerFactory.AddProvider(new ElasticLoggerProvider(_hostingEnvironment,projectInfo,_httpContextAccessor,_requestContext,_user,_elasticSearchprovider));
                }

                if (logConfig.Value.EnableDebugLogProvider)
                {
                    _loggerFactory.AddProvider(new DebugLoggerProvider(_hostingEnvironment, projectInfo, _httpContextAccessor, _requestContext, _user));
                }

            }




            return services;
        }
    }
}
