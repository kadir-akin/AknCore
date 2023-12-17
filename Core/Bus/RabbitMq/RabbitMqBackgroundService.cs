using Core.Bus.Abstract;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Bus.RabbitMq
{
    public class RabbitMqBackgroundService : BackgroundService
    {
        private readonly IRabbitMqProvider _rabbitMqProvider;
        private readonly IBusContext _busContext;
        public RabbitMqBackgroundService(IRabbitMqProvider rabbitMqProvider, IBusContext busContext)
        {
            _rabbitMqProvider = rabbitMqProvider;
            _busContext = busContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_rabbitMqProvider.Consume();
                await StopAsync(stoppingToken);

            }
        }
    }
}
