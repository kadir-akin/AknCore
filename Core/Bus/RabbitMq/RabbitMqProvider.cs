using Core.Bus.Abstract;
using Core.Utilities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.RabbitMq
{
    public class RabbitMqProvider : IRabbitMqProvider
    {
        private readonly IOptions<RabbitMqConfiguration> _options;
        private readonly IBusContext _busContext;
        private  IConnection _connection;
        public RabbitMqProvider(IOptions<RabbitMqConfiguration> options, IBusContext busContext)
        {
            _options = options;
            _busContext = busContext;
        }

        public IConnection GetConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();

            connectionFactory.UserName = _options.Value.UserName;
            connectionFactory.HostName = _options.Value.HostName;
            connectionFactory.Port = Convert.ToInt32(_options.Value.Port);
            connectionFactory.Password = _options.Value.Password;

            _connection= connectionFactory.CreateConnection();
            return _connection;
        }
        public Task Publish(IBusMessage message)
        {
            using (IConnection connection = GetConnection())
            using (IModel channel = connection.CreateModel())
            {
                var busMessageAtrtribute = _busContext.GetRabbitMqContextFromBusMessageType(message.GetType())?.Attribute;
                channel.QueueDeclare(busMessageAtrtribute.Queue, busMessageAtrtribute.Durable, busMessageAtrtribute.Exclusive, busMessageAtrtribute.AutoDelete, null);

                byte[] bytemessage = Encoding.UTF8.GetBytes(message.ToJson());

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = busMessageAtrtribute.Persistent;

                channel.BasicPublish(exchange: "", routingKey: busMessageAtrtribute.Queue, basicProperties: properties, body: bytemessage);
                ConnectionClose();
            }

            return null;
        }

        public Task Consume(Type consumeType)
        {
            using (IConnection connection = GetConnection())
            using (IModel channel = connection.CreateModel())
            {
                var context = _busContext.GetRabbitMqContextFromBusMessageType(consumeType);
                var busMessageAtrtribute = context?.Attribute;
                channel.QueueDeclare(busMessageAtrtribute.Queue, busMessageAtrtribute.Durable, busMessageAtrtribute.Exclusive, busMessageAtrtribute.AutoDelete, null);
                channel.BasicQos(prefetchSize: busMessageAtrtribute.PrefetchSize, prefetchCount: busMessageAtrtribute.PrefetchCount, global: busMessageAtrtribute.Global);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(busMessageAtrtribute.Queue, busMessageAtrtribute.AutoAck, consumer);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var convertMessage =message.ToBusObject(context.BusMessage);
                    context.ConsumeHandler.HandleAsync(convertMessage);
                    channel.BasicAck(e.DeliveryTag, false);
                   
                };
            }
           return Task.CompletedTask;
        }

        public void ConnectionClose() 
        {
            _connection.Close();       
        }
    }
}
