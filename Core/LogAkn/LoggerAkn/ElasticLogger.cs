using Core.Elastic.Abstract;
using Core.LogAkn.Abstract;
using Core.LogAkn.Concrate;
using Core.RequestContext.Concrate;
using Core.Security.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.LogAkn.LoggerAkn
{
    internal class ElasticLogger : IAknLogger
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
        
        public Task LogAsync(LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            if (args != null && args.Any())
            {
                message = string.Format(message, args);
            }

            var log = new RequestContextLog(message, logLevel.ToString(), _httpContext, exception, _requestContext, _user, _projectInfoConfiguration.Value);
            _elasticSearchProvider.ChekIndex();
            _elasticSearchProvider.InsertDocument(log);
            return Task.CompletedTask;
        }
    }
}
