using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.LogAkn.Abstract
{
    public  interface IAknLogger
    {
        public Task LogAsync(LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args);
    }
}
