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
    public class RabbitMqProvider<T> : IRabbitMqProvider<T> where T : class, IBusMessage
    {
        private readonly IOptions<RabbitMqConfiguration> _options;
        private readonly IBusContext _busContext;
        public RabbitMqContext RabbitMqContext { get; set; }

        public IModel ConsumeChannel { get; set; }

        public IConnection ConsumeConnection { get; set; }
        public RabbitMqProvider(IOptions<RabbitMqConfiguration> options, IBusContext busContext)
        {
            _options = options;
            _busContext = busContext;
            RabbitMqContext = _busContext.GetRabbitMqContextFromBusMessageType(typeof(T));
        }

        public ConnectionFactory GetConnectionFactory()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();

            connectionFactory.UserName = _options.Value.UserName;
            connectionFactory.HostName = _options.Value.HostName;
            connectionFactory.Port = Convert.ToInt32(_options.Value.Port);
            connectionFactory.Password = _options.Value.Password;
            return connectionFactory;

        }
        public Task Publish(T message)
        {
            using (IConnection connection = GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    var busMessageAtrtribute = RabbitMqContext?.Attribute;
                    channel.QueueDeclare(busMessageAtrtribute.Queue, busMessageAtrtribute.Durable, busMessageAtrtribute.Exclusive, busMessageAtrtribute.AutoDelete, null);

                    byte[] bytemessage = Encoding.UTF8.GetBytes(message.ToJson());

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = busMessageAtrtribute.Persistent;

                    channel.BasicPublish(exchange: "", routingKey: busMessageAtrtribute.Queue, basicProperties: properties, body: bytemessage);
                }
            }


            return null;
        }

        public Task Consume()
        {
            using (ConsumeConnection = GetConnectionFactory().CreateConnection())
            {
                using (ConsumeChannel = ConsumeConnection.CreateModel())
                {
                    var busMessageAtrtribute = RabbitMqContext?.Attribute;
                    ConsumeChannel.QueueDeclare(busMessageAtrtribute.Queue, busMessageAtrtribute.Durable, busMessageAtrtribute.Exclusive, busMessageAtrtribute.AutoDelete, null);
                    ConsumeChannel.BasicQos(prefetchSize: busMessageAtrtribute.PrefetchSize, prefetchCount: busMessageAtrtribute.PrefetchCount, global: busMessageAtrtribute.Global);

                    EventingBasicConsumer consumer = new EventingBasicConsumer(ConsumeChannel);
                    ConsumeChannel.BasicConsume(busMessageAtrtribute.Queue, busMessageAtrtribute.AutoAck, consumer);
                    
                    consumer.Received += HandleMessageReceived;
                    //consumer.Registered += ConsumerRegistered;
                }
            }

            return Task.CompletedTask;
        }

        private void ConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandleMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            lock (ConsumeConnection)
            {
                lock (ConsumeChannel)
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var convertMessage = message.ToBusObject(RabbitMqContext?.BusMessage);
                    RabbitMqContext?.ConsumeHandler.HandleAsync(convertMessage);
                    ConsumeChannel.BasicAck(e.DeliveryTag, false);
                }
            }
           


        }
    }
}
