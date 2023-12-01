using Core.LogAkn.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Concrate
{
    public class AknLogService :ILogService
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IDebugLoggerProvider _debugLoggerProvider;
        private readonly IElasticLoggerProvider  _elasticLoggerProvider;
        private readonly ILogger _logger;
        private readonly IOptions<LogConfiguration> _logConfig;
        public AknLogService(ILoggerFactory loggerFactory, IDebugLoggerProvider debugLoggerProvider, IElasticLoggerProvider elasticLoggerProvider,IOptions<LogConfiguration> logConfig)
        {
            _loggerFactory = loggerFactory;
            _debugLoggerProvider = debugLoggerProvider;
            _elasticLoggerProvider = elasticLoggerProvider;
            _logConfig = logConfig;
            
            if(_logConfig.Value.EnableDebugLogProvider)            
            _loggerFactory.AddProvider(_debugLoggerProvider);
            
            if(_logConfig.Value.EnableElasticLogProvider)
            _loggerFactory.AddProvider(_elasticLoggerProvider);

            _logger = _loggerFactory.CreateLogger("");
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log(LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            _logger.Log(logLevel, eventId,exception, message, args);
        }
    }

    
}
