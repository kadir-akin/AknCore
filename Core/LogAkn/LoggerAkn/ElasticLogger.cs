using Core.Elastic.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Log.Logger
{
    public class ElasticLogger :ILogger
    {
        IHostingEnvironment _hostingEnvironment;
        
        public ElasticLogger(IHostingEnvironment hostingEnvironment) 
        { 
            _hostingEnvironment = hostingEnvironment; 
        }
        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => true;
        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, System.Exception exception, Func<TState, System.Exception, string> formatter)
        {
            //using (StreamWriter streamWriter = new StreamWriter($"{_hostingEnvironment.ContentRootPath}/log.txt", true))
            //{
            //    await streamWriter.WriteLineAsync($"Log Level : {logLevel.ToString()} | Event ID : {eventId.Id} | Event Name : {eventId.Name} | Formatter : {formatter(state, exception)}");
            //    streamWriter.Close();
            //    await streamWriter.DisposeAsync();
            //}
        }
    }
}
