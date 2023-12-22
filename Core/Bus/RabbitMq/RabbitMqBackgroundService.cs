using Core.Bus.Abstract;
using Core.LogAkn.Abstract;
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
        private readonly IServiceProvider _serviceProvider;
        public RabbitMqBackgroundService(IServiceProvider serviceProvider, IBusContext busContext)
        {
            _rabbitMqProvider = (IRabbitMqProvider<T>)serviceProvider.GetService(typeof(IRabbitMqProvider<T>));
            _busContext = busContext;   
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        { 
             await Task.Yield();         
             await HandleConsumer(stoppingToken);
             await StopAsync(stoppingToken);

        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {            
            return base.StopAsync(cancellationToken);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {         
            return base.StartAsync(cancellationToken);
        }
        private async Task HandleConsumer(CancellationToken stoppingToken) 
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (await _rabbitMqProvider.MessageCount() == 0)
                {
                    await Task.Delay(1000);
                }
                await _rabbitMqProvider.Consume();
            }            

        }
    }
}
