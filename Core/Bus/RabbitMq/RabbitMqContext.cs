using Core.Bus.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bus.RabbitMq
{
    public class RabbitMqContext
    {
        public Type BusMessage { get; set; }

        public RabbitMqAttribute Attribute { get; set; }

        public IConsumeHandler ConsumeHandler { get; set; }


        public RabbitMqContext()
        {

        }


        public RabbitMqContext(Type BusMessage, RabbitMqAttribute Attribute,IConsumeHandler ConsumeHandler)
        {
            this.BusMessage = BusMessage;
            this.Attribute = Attribute;
            this.ConsumeHandler = ConsumeHandler;
        }
    }
}
