using Core.Bus.RabbitMq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Abstract
{
    public interface IRabbitMqProvider<T> where T : class,IBusMessage
    {
        public ConnectionFactory GetConnectionFactory();
        public Task Publish(T message);
        public Task Consume();

        public Task<uint> MessageCount();

    }
}
