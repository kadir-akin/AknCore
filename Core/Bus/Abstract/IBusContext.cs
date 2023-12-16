using Core.Bus.RabbitMq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bus.Abstract
{
    public interface IBusContext
    {
        public List<Type> AssembyBusMessageList { get; set; }
        public Dictionary<Type, RabbitMqAttribute> RabbitMqAttributeValues { get; set; }
    }
}
