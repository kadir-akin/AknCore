using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Abstract
{
    public interface IRabbitMqProvider
    {
        public ConnectionFactory GetConnectionFactory();
        public Task Publish(IBusMessage message);
        public Task Consume(Type consumeType);
    }
}
