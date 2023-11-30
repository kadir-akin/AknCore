using Core.Elastic.Abstract;
using Core.LogAkn.Concrate;
using Core.RequestContext.Concrate;
using Core.Security.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.LoggerAkn
{
    internal class ElasticLogger :ILogger
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOptions<ProjectInfoConfiguration> _projectInfoConfiguration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAknRequestContext _requestContext;
        private readonly IAknUser _user;
        private readonly IElasticSearchProvider<RequestContextLog> _elasticSearchProvider;
        public ElasticLogger(
            IHostingEnvironment hostingEnvironment,
            IOptions<ProjectInfoConfiguration> projectInfoConfiguration,
            IHttpContextAccessor httpContext,
            IAknRequestContext requestContext,
            IAknUser user,
            IElasticSearchProvider<RequestContextLog> elasticSearchProvider
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _projectInfoConfiguration = projectInfoConfiguration;
            _httpContext = httpContext;
            _requestContext = requestContext;
            _user = user;
            _elasticSearchProvider = elasticSearchProvider;
            _elasticSearchProvider.ChekIndex().GetAwaiter().GetResult();
        }

        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => true;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, System.Exception exception, Func<TState, System.Exception, string> formatter)
        {
            if (_httpContext.HttpContext == null)
                return;
             var log = new RequestContextLog(formatter(state, exception), logLevel.ToString(), _httpContext, exception, _requestContext, _user, _projectInfoConfiguration.Value);
            _elasticSearchProvider.ChekIndex();
            _elasticSearchProvider.InsertDocument(log);
        }
    }
}
