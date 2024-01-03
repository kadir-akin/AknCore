using Core.Database.Mongo.Abstract;
using Core.Database.Mongo.Concrate;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Template_Api
{
    public interface IMongoExampleRepository :IMongoRepository<MongoExampleCollection>
    {
    }
    public class MongoExampleRepository : MongoRepository<MongoExampleCollection>, IMongoExampleRepository
    {
        public MongoExampleRepository(IOptions<MongoConfiguration> mongoConfig, IMongoDatabase mongoDatabase) : base(mongoConfig, mongoDatabase)
        {
        }
    }

    public interface IMongoCustomerRepository : IMongoRepository<MongoCustomerCollection>
    {
    }
    public class MongoCustomerRepository : MongoRepository<MongoCustomerCollection>, IMongoCustomerRepository
    {
        public MongoCustomerRepository(IOptions<MongoConfiguration> mongoConfig, IMongoDatabase mongoDatabase) : base(mongoConfig, mongoDatabase)
        {
        }
    }
}
