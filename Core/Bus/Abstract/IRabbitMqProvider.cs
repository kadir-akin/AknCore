﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Abstract
{
    public interface IRabbitMqProvider
    {
        public IConnection GetConnection();
        public Task Publish(IBusMessage message);
    }
}
