using Core.Bus.Abstract;
using Core.Bus.RabbitMq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bus.Concrate
{
    public class BusContext :IBusContext
    {
        public BusContext(List<Type> assembyBusMessageList, Dictionary<Type, RabbitMqAttribute> rabbitMqAttributeValues)
        {
            AssembyBusMessageList = assembyBusMessageList;
            RabbitMqAttributeValues = rabbitMqAttributeValues;
        }
        public List<Type> AssembyBusMessageList { get; set; }

        public Dictionary<Type, RabbitMqAttribute> RabbitMqAttributeValues { get; set; }
    }
}
