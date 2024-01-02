using Core.Database.Mongo.Abstract;
using Core.Database.Mongo.Concrate;
using System;

namespace Template_Api
{
    public class MongoExampleCollection : AknMongoCollection
    {
        public string Name { get; set; }

        public string FirstName { get; set; }
        public int Age { get; set; } 
        public DateTime CreateDate { get; set; } 


    }


    public class MongoCustomerCollection : AknMongoCollection
    {
        public string Name { get; set; }

        public string FirstName { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;


    }
}
