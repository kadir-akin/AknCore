using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Abstract
{
    public interface ILogContext
    {
        public List<IAknLoggerProvider> TotalProviderList { get; set; }
        public List<IAknLogger> LoggerList { get; set; }
    }
}
