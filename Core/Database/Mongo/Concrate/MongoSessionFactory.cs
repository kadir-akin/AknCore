using Core.Database.Mongo.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Mongo.Concrate
{
    public class MongoSessionFactory : IMongoSessionFactory
    {
        private readonly IMongoClient _mongoClient;
        public MongoSessionFactory(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient; 
        }
        public Task<IClientSessionHandle> CreateTaskAsync()
        {
            return _mongoClient.StartSessionAsync();
        }
    }
}
