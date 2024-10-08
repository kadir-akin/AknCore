﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bus.RabbitMq
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Port { get; set; }

        public bool CosumerTaskDelayEnable { get; set; }
    }
}
