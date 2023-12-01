using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Abstract
{
    public interface ILogService
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, System.Exception exception, Func<TState, System.Exception, string> formatter);

    }
}
