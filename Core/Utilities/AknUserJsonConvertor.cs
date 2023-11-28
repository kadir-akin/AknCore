using Core.Security.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities
{

    public class AknUserJsonConvertor : JsonConverter
    {
        private readonly Type AknDependencyType;
        public AknUserJsonConvertor(Type aknDependencyType)
        {
            AknDependencyType = aknDependencyType;
        }
        public override bool CanConvert(Type objectType)
        {
            return (objectType ==typeof( IAknUser));
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, AknDependencyType);
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value, AknDependencyType);
        }

        public static JsonSerializerSettings GetJsonSerializerSettings(Type type)
        {
            return new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>()
                {
                  new AknUserJsonConvertor(type)
                }
            };
        }
    }
}



