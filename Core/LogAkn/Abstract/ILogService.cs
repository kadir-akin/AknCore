using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Abstract
{
    public interface ILogService
    {
        public void Log( LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args);
    }
}
