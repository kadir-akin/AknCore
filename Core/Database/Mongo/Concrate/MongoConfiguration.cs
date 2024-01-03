using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.Mongo.Concrate
{
    public class MongoConfiguration
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
