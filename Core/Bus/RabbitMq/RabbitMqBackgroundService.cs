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
                                  
             await HandleConsumer(stoppingToken);
             await StopAsync(stoppingToken);

        }

        private async Task HandleConsumer(CancellationToken stoppingToken) 
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (await _rabbitMqProvider.MessageCount() == 0)
                {
                    await Task.Delay(5000);
                }
                await _rabbitMqProvider.Consume();
            }            

        }
    }
}
