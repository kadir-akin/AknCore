using Core.LogAkn.Abstract;
using Microsoft.Extensions.Logging;
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
        public AknLogService(ILoggerFactory loggerFactory, IDebugLoggerProvider debugLoggerProvider, IElasticLoggerProvider elasticLoggerProvider)
        {
            _loggerFactory = loggerFactory;
            _debugLoggerProvider = debugLoggerProvider;
            _elasticLoggerProvider = elasticLoggerProvider;
            _loggerFactory.AddProvider(_elasticLoggerProvider);
            _loggerFactory.AddProvider(_debugLoggerProvider);
            _logger = _loggerFactory.CreateLogger("");
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, System.Exception exception, Func<TState, System.Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }

    
}
