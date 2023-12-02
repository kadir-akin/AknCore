using Core.LogAkn.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.LogAkn.Concrate
{
    public class AknLogService :ILogService
    {
        private readonly IAknLoggerFactory _loggerFactory;
        private readonly IDebugLoggerProvider _debugLoggerProvider;
        private readonly IElasticLoggerProvider  _elasticLoggerProvider;
        private readonly IAknLogger _logger;
        private readonly IOptions<LogConfiguration> _logConfig;
        private readonly ILogContext _logContext;


        public AknLogService(IAknLoggerFactory loggerFactory, ILogContext logContext, IDebugLoggerProvider debugLoggerProvider, IElasticLoggerProvider elasticLoggerProvider,IOptions<LogConfiguration> logConfig)
        {
            _loggerFactory = loggerFactory;
            _debugLoggerProvider = debugLoggerProvider;
            _elasticLoggerProvider = elasticLoggerProvider;
            _logConfig = logConfig;
            _logContext = logContext;

            _logContext.TotalProviderList.Clear();
            _logContext.LoggerList.Clear();
            if (_logConfig.Value.EnableDebugLogProvider) 
            {               
                _loggerFactory.AddLoggerProvider(_debugLoggerProvider);
            }

            if (_logConfig.Value.EnableElasticLogProvider) 
            {
                _loggerFactory.AddLoggerProvider(_elasticLoggerProvider);
            }

          
        }
      

        public Task LogAsync(LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            _loggerFactory.InvokeAsync(logLevel, eventId,exception, message, args);
            return Task.CompletedTask;
        }
    }

    
}
