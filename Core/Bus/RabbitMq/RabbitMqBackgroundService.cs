using Core.Bus.Abstract;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bus.RabbitMq
{
    public class RabbitMqBackgroundService<T> : BackgroundService where T : class,IBusMessage
    {
        private readonly IRabbitMqProvider<T> _rabbitMqProvider;
        private readonly IBusContext _busContext;
        public RabbitMqBackgroundService(IServiceProvider serviceProvider, IBusContext busContext)
        {
            _rabbitMqProvider = (IRabbitMqProvider<T>)serviceProvider.GetService(typeof(IRabbitMqProvider<T>));
            _busContext = busContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            while (!stoppingToken.IsCancellationRequested)
            {
                
               await _rabbitMqProvider.Consume();
            }

               
            await StopAsync(stoppingToken);

        }
    }
}
