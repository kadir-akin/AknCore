using Core.LogAkn.Abstract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.LogAkn.Concrate
{
    public class AknLoggerFactory : IAknLoggerFactory
    {
        private ILogContext _logContext;
        public AknLoggerFactory(ILogContext logContext)
        {
            _logContext = logContext;
        }
        public List<IAknLogger> AddLoggerProvider(IAknLoggerProvider provider)
        {
            _logContext.TotalProviderList.Add(provider);
            _logContext.LoggerList.Add(provider.CreateLogger());
            return _logContext.LoggerList;

        }
               
        public Task InvokeAsync(LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            if (_logContext.LoggerList.Any())
            {
                foreach (var item in _logContext.LoggerList)
                {
                    item.LogAsync(logLevel, eventId, exception, message, args);
                }
            }

            return Task.CompletedTask;
        }
    }
}
