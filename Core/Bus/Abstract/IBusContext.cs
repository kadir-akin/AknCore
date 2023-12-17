using Core.Bus.RabbitMq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bus.Abstract
{
    public interface IBusContext
    {
        public List<Type> AssembyBusMessageList { get; set; }
        public List<RabbitMqContext> RabbitMqContextList { get; set; }

        public RabbitMqContext GetRabbitMqContextFromBusMessageType(Type busMessageType);
    }
}
