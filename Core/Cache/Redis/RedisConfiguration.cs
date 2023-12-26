using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache.Redis
{
    public class RedisConfiguration
    {
        public string Host { get; set; }

        public bool AllowAdmin { get; set; }

        public string Password { get; set; }

        public int Database { get; set; }

        public bool MemoryFirst { get; set; }


        public string ClientName { get; set; }

    }
}
