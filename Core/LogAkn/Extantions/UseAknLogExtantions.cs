﻿using Core.Elastic.Abstract;
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
            var projectInfo = (IOptions<ProjectInfoConfiguration>)services.GetService(typeof(IOptions<ProjectInfoConfiguration>));
            var logConfig =(IOptions<LogConfiguration>) services.GetService(typeof(IOptions<LogConfiguration>));
            var _loggerFactory =(ILoggerFactory)services.GetService(typeof(ILoggerFactory));
           
            if (projectInfo.Value.EnableLog)
            {
               
                var _hostingEnvironment = (IHostingEnvironment)services.GetService(typeof(IHostingEnvironment));
                var _httpContextAccessor =(IHttpContextAccessor) services.GetService(typeof(IHttpContextAccessor));
                               
                if (logConfig.Value.EnableElasticLogProvider)
                {                   
                    _loggerFactory.AddProvider(elasticLoggerProvider);
                    
                }

                if (logConfig.Value.EnableDebugLogProvider)
                {
                    _loggerFactory.AddProvider(_debugLoggerProvider);
                }

            }
        }
    }
}
