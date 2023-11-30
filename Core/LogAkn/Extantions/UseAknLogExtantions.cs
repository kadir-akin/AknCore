using Core.Elastic.Abstract;
using Core.LogAkn.Concrate;
using Core.LogAkn.LoggerProvider;
using Core.RequestContext.Concrate;
using Core.Security.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Extantions
{
    public static class UseAknLogExtantions
    {
        public static void UseAknLogProvider(this IApplicationBuilder app)
        {
            var services = app.ApplicationServices;
            var projectInfo = (IOptions<ProjectInfoConfiguration>)services.GetService(typeof(IOptions<ProjectInfoConfiguration>));
            var logConfig =(IOptions<LogConfiguration>) services.GetService(typeof(IOptions<LogConfiguration>));
            var _loggerFactory =(ILoggerFactory)services.GetService(typeof(ILoggerFactory));
           
            if (projectInfo.Value.EnableLog)
            {                
                var _hostingEnvironment = (IHostingEnvironment)services.GetService(typeof(IHostingEnvironment));
                var _httpContextAccessor =(IHttpContextAccessor) services.GetService(typeof(IHttpContextAccessor));
                var _requestContext =(IAknRequestContext) services.GetService(typeof(IAknRequestContext));
                var _user =(IAknUser) services.GetService(typeof(IAknUser));

                if (logConfig.Value.EnableElasticLogProvider)
                {
                    var _elasticSearchprovider = (IElasticSearchProvider<RequestContextLog>)services.GetService(typeof(IElasticSearchProvider<RequestContextLog>));
                    _loggerFactory.AddProvider(new ElasticLoggerProvider(_hostingEnvironment, projectInfo, _httpContextAccessor, _requestContext, _user, _elasticSearchprovider));
                }

                if (logConfig.Value.EnableDebugLogProvider)
                {
                    _loggerFactory.AddProvider(new DebugLoggerProvider(_hostingEnvironment, projectInfo, _httpContextAccessor, _requestContext, _user));
                }

            }
        }
    }
}
