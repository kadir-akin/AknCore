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
        private readonly IOptions<LogConfiguration> _logConfig;
        private readonly ILogContext _logContext;
        private readonly IServiceProvider _serviceProvider;

        public AknLogService(IAknLoggerFactory loggerFactory, ILogContext logContext,IOptions<LogConfiguration> logConfig,IServiceProvider serviceProvider)
        {
            _loggerFactory = loggerFactory;          
            _logConfig = logConfig;
            _logContext = logContext;
            _serviceProvider = serviceProvider;
            Initiliaze();
        }
      

        public Task LogAsync(LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            _loggerFactory.InvokeAsync(logLevel, eventId,exception, message, args);
            return Task.CompletedTask;
        }

        private void Initiliaze ()
        {

            _logContext.TotalProviderList.Clear();
            _logContext.LoggerList.Clear();
            if (_logConfig.Value.EnableDebugLogProvider)
            {
                var debugprovider = (IDebugLoggerProvider)_serviceProvider.GetService(typeof(IDebugLoggerProvider));
                _loggerFactory.AddLoggerProvider(debugprovider);
            }

            if (_logConfig.Value.EnableElasticLogProvider)
            {
                var elasticprovider = (IElasticLoggerProvider)_serviceProvider.GetService(typeof(IElasticLoggerProvider));
                _loggerFactory.AddLoggerProvider(elasticprovider);
            }


        }
    }

    
}
