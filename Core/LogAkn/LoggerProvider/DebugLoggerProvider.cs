using Core.LogAkn.LoggerAkn;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.LoggerProvider
{
    public class DebugLoggerProvider : ILoggerProvider
    {
        public IHostingEnvironment _hostingEnvironment;
        public DebugLoggerProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ILogger CreateLogger(string categoryName) => new DebugLogger(_hostingEnvironment);
        public void Dispose() => throw new NotImplementedException();
    }

}
