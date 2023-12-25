using Core.Cache.Concrate;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities
{
    public static class RedisUtilities
    {
        public static HashEntry[] ToHashEntries(string key,object obj,TimeSpan? expiry)
        {
            var entryList = new  List<HashEntry>();
            entryList.Add( new HashEntry("Value", JsonConvert.SerializeObject(obj)));
            entryList.Add(new HashEntry("Key", key));                       
            
            var nowDateTime = DateTime.Now;           
            entryList.Add(new HashEntry("CreateDate", nowDateTime.ToString("G")));

            var expireDate = nowDateTime.Add(expiry ?? TimeSpan.FromDays(1));
            entryList.Add(new HashEntry("ExpirationDate", expireDate.ToString("G")));
            entryList.Add(new HashEntry("ExpirationTotalMinutes", (expiry ?? TimeSpan.FromDays(1)).TotalMinutes));
           
            return entryList.ToArray();
           
        }

        public static T HashEntryToObject<T>(HashEntry[] hashEntries)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
                if (entry.Equals(new HashEntry())) continue;
                property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
            }
            return (T)obj;
        }

        public static T ToObjectCacheEntryHashEntrys<T>(HashEntry[] hashEntries) 
        {
           var cacheEntry=  HashEntryToObject<CacheEntry>(hashEntries);
           var value = cacheEntry?.Value;

            if (!string.IsNullOrEmpty(value))
                return value.ToObject<T>();

            return default(T);


        }

        public static T  GetData<T>(this CacheEntry cacheEntry) 
        {
            if (cacheEntry == null) return default(T);

            var value = cacheEntry?.Value;

            if (!string.IsNullOrEmpty(value))
                return value.ToObject<T>();

            return default(T);
        }

      
    }
}
