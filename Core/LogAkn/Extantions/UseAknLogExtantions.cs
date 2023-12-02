using Core.Elastic.Abstract;
using Core.LogAkn.Abstract;
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
        public static void UseAknLogProvider(this IApplicationBuilder app, IDebugLoggerProvider   _debugLoggerProvider, IElasticLoggerProvider  elasticLoggerProvider)
        {
            var services = app.ApplicationServices;        
            var logConfig =(IOptions<LogConfiguration>) services.GetService(typeof(IOptions<LogConfiguration>));
            var _loggerFactory =(IAknLoggerFactory)services.GetService(typeof(IAknLoggerFactory));
           
            if (logConfig.Value.EnableLog)
            {                                                           
                if (logConfig.Value.EnableElasticLogProvider)
                {                   
                    _loggerFactory.AddLoggerProvider(elasticLoggerProvider);                    
                }

                if (logConfig.Value.EnableDebugLogProvider)
                {
                    _loggerFactory.AddLoggerProvider(_debugLoggerProvider);
                }
            }
        }
    }
}
