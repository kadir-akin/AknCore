using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache.Concrate
{
    public class CacheEntry
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string ExpirationDate { get; set; }
        
    }
}
