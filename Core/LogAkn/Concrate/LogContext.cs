using Core.LogAkn.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Concrate
{
    public  class LogContext :ILogContext
    {
        public List<IAknLoggerProvider> DebugProviderList { get; set; } =  new List<IAknLoggerProvider> ();
        public List<IAknLoggerProvider> ElasticProviderList { get; set; } = new List<IAknLoggerProvider>();

        public List<IAknLoggerProvider> TotalProviderList { get; set; } = new List<IAknLoggerProvider>();
        public List<IAknLogger> LoggerList { get; set; } = new List<IAknLogger>();
    }
}
