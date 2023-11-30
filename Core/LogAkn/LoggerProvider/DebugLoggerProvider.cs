using Core.LogAkn.Abstract;
using Core.LogAkn.Concrate;
using Core.LogAkn.LoggerAkn;
using Core.RequestContext.Concrate;
using Core.Security.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.LoggerProvider
{
    public class DebugLoggerProvider : IDebugLoggerProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOptions<ProjectInfoConfiguration> _projectInfoConfiguration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAknRequestContext _requestContext;
        private readonly IAknUser _user;
        private readonly List<DebugLogger> debugLoggerProviders = new List<DebugLogger>();
        public DebugLoggerProvider(
            IHostingEnvironment hostingEnvironment,
            IOptions<ProjectInfoConfiguration> projectInfoConfiguration,
            IHttpContextAccessor httpContext,
            IAknRequestContext requestContext,
            IAknUser user
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _projectInfoConfiguration = projectInfoConfiguration;
            _httpContext = httpContext;
            _requestContext = requestContext;
            _user = user;

        }
        public ILogger CreateLogger(string categoryName)
        {
            var logger = new DebugLogger(_hostingEnvironment, _projectInfoConfiguration, _httpContext, _requestContext, _user);
            debugLoggerProviders.Add(logger);
            return logger;
        }
        public void Dispose() => debugLoggerProviders.Clear();
    }

}
