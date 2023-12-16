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


    }
}
