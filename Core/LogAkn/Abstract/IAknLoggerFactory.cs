using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.LogAkn.Abstract
{
    public interface IAknLoggerFactory
    {
        public List<IAknLogger> AddLoggerProvider(IAknLoggerProvider provider);

        public Task InvokeAsync(LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args);
    }
}
