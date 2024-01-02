using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.Mongo.Abstract
{
    public interface IAknMongoCollection
    {
      
        public string Id { get; set; }
    }
}
