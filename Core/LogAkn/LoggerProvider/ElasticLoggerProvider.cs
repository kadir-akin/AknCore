using Core.Elastic.Abstract;
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
    public class ElasticLoggerProvider : ILoggerProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOptions<ProjectInfoConfiguration> _projectInfoConfiguration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAknRequestContext _requestContext;
        private readonly IAknUser _user;
        private readonly IElasticSearchProvider<RequestContextLog> _elasticSearchProvider;
        public ElasticLoggerProvider(
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

        }
        public ILogger CreateLogger(string categoryName) => new ElasticLogger(_hostingEnvironment, _projectInfoConfiguration, _httpContext, _requestContext, _user,_elasticSearchProvider);
        public void Dispose() => throw new NotImplementedException();
    }
}
