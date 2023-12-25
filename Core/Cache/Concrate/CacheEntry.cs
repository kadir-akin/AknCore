using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache.Concrate
{
    public class CacheEntry
    {
        public string Key { get; set; }
        public string Value { get; set; }       
        public string CreateDate { get; set; }
        public string ExpirationDate { get; set; }
        public double ExpirationTotalMinutes { get; set; }



        public CacheEntry()
        {

        }


        public CacheEntry(string key,object data,TimeSpan? timeSpan)
        {
            Key=key;
            Value = data.ToJsonString();

            var dateTime =DateTime.Now;
            var expireDate = dateTime.Add(timeSpan ?? TimeSpan.MaxValue);
            CreateDate = dateTime.ToString("G");
            ExpirationDate = expireDate.ToString("G");
            ExpirationTotalMinutes = (timeSpan ?? TimeSpan.MaxValue).TotalMinutes;

        }

    }
}
