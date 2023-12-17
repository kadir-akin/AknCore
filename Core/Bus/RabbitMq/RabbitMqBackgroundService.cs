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
    public class RabbitMqBackgroundService : BackgroundService
    {
        private readonly IRabbitMqProvider _rabbitMqProvider;
        private readonly IBusContext _busContext;
        public RabbitMqBackgroundService(IServiceProvider serviceProvider, IBusContext busContext)
        {
            _rabbitMqProvider = (IRabbitMqProvider)serviceProvider.GetService(typeof(IRabbitMqProvider));
            _busContext = busContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            while (!stoppingToken.IsCancellationRequested)
            {
                
               await _rabbitMqProvider.Consume(_busContext.RabbitMqContextList.FirstOrDefault().BusMessage);
            }

               
            await StopAsync(stoppingToken);

        }
    }
}
