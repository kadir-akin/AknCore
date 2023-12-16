using Core.Bus.Abstract;
using Core.Utilities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.RabbitMq
{
    public class RabbitMqProvider : IRabbitMqProvider
    {
        private readonly IOptions<RabbitMqConfiguration> _options;
        private readonly IBusContext _busContext;
        public RabbitMqProvider(IOptions<RabbitMqConfiguration> options,IBusContext busContext)
        {
            _options = options;
            _busContext = busContext;
        }

        public IConnection GetConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                UserName = _options.Value.UserName,
                HostName = _options.Value.HostName,
                Port = Convert.ToInt32(_options.Value.Port),
                Password = _options.Value.Password,
            };
            return connectionFactory.CreateConnection();
        }
        public Task Publish(IBusMessage message)
        {
            using (IConnection connection = GetConnection())
            using (IModel channel = connection.CreateModel())
            {
                var busMessageAtrtribute = _busContext.RabbitMqAttributeValues[message.GetType()];
                channel.QueueDeclare(busMessageAtrtribute.Queue, busMessageAtrtribute.Durable, busMessageAtrtribute.Exclusive, busMessageAtrtribute.AutoDelete, null);

                byte[] bytemessage = Encoding.UTF8.GetBytes(message.ToJson());

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = busMessageAtrtribute.Persistent;

                channel.BasicPublish(exchange: "", routingKey: busMessageAtrtribute.Queue, basicProperties: properties, body: bytemessage);               
            }

            return null;
        }

        public Task Consume(Type consumeType) 
        {
            using (IConnection connection = GetConnection())
            using (IModel channel = connection.CreateModel())
            {
                var busMessageAtrtribute = _busContext.RabbitMqAttributeValues[consumeType];
                channel.QueueDeclare(busMessageAtrtribute.Queue, busMessageAtrtribute.Durable, busMessageAtrtribute.Exclusive, busMessageAtrtribute.AutoDelete, null);
                channel.BasicQos(prefetchSize: busMessageAtrtribute.PrefetchSize, prefetchCount: busMessageAtrtribute.PrefetchCount, global: busMessageAtrtribute.Global);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(busMessageAtrtribute.Queue, busMessageAtrtribute.AutoAck, consumer);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                   
                    channel.BasicAck(e.DeliveryTag, false);
                };
                Console.Read();
            }
            return null;
        }
    }
}
