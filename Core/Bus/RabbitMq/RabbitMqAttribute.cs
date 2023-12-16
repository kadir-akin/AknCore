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
        public RabbitMqAttribute(string queue,bool durable,bool exclusive,bool autoDelete,bool persistent,string rountingKey, uint prefetchSize,ushort prefetchCount,bool global, bool autoAck)
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
