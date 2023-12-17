using Core.Bus.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities
{
    public static class JsonConvertUtilites
    {
        public static string ToJson(this IBusMessage message) 
        {
            if (message == null)
                return null;

            var result =JsonConvert.SerializeObject(message);
            return result;
        }

        public static TObject ToObject<TObject>(this string message) 
        {

            if (string.IsNullOrEmpty(message))
            {
                return default(TObject);
            }

            return JsonConvert.DeserializeObject<TObject>(message);
        }

        public static IBusMessage ToBusObject(this string message,Type convertType) 
        {
          return  JsonConvert.DeserializeObject<IBusMessage>(message, BusMessageJsonConvertor.GetJsonSerializerSettings(convertType));
        }
    }
}
