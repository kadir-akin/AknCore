using Core.Bus.Abstract;
using Core.Bus.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Bus.Concrate
{
    public class BusContext :IBusContext
    {
        public BusContext(List<Type> assembyBusMessageList, List<RabbitMqContext> rabbitMqContextList)
        {
            AssembyBusMessageList = assembyBusMessageList;
            RabbitMqContextList = rabbitMqContextList;
        }
        public List<Type> AssembyBusMessageList { get; set; }

        public List<RabbitMqContext> RabbitMqContextList { get; set; }

        public RabbitMqContext GetRabbitMqContextFromBusMessageType(Type busMessageType)
        {
           return RabbitMqContextList.Where(x => x.BusMessage == busMessageType).FirstOrDefault();
        }
    }
}
