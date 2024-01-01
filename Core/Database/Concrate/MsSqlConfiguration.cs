using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.Concrate
{
    public class MsSqlConfiguration
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool UseUnitOfWork { get; set; }
    }
}
