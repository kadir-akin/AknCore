using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bus.RabbitMq
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RabbitMqAttribute :Attribute
    {
        public string Queue { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public bool Persistent { get; set; }
        public string RoutingKey { get; set; }
        public uint PrefetchSize { get; set; }
        public ushort PrefetchCount { get; set; }
        public bool Global { get; set; }

        public bool AutoAck { get; set; }
        public RabbitMqAttribute(string queue, string rountingKey,bool durable=false,bool exclusive=false,bool autoDelete=false,bool persistent=true, uint prefetchSize=0,ushort prefetchCount=1,bool global=false, bool autoAck=false)
        {
            Queue = queue;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
            Durable = durable;
            Persistent = persistent;
            RoutingKey = rountingKey;
            PrefetchCount = prefetchCount;
            PrefetchSize = prefetchSize;
            Global = global;
            AutoAck = autoAck;
        }
    }
}
