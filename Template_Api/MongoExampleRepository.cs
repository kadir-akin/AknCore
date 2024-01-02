using Core.Database.Mongo.Abstract;
using Core.Database.Mongo.Concrate;
using Microsoft.Extensions.Options;

namespace Template_Api
{
    public interface IMongoExampleRepository :IMongoRepository<MongoExampleCollection>
    {
    }
    public class MongoExampleRepository : MongoRepository<MongoExampleCollection>, IMongoExampleRepository
    {
        public MongoExampleRepository(IOptions<MongoConfiguration> mongoConfig) : base(mongoConfig)
        {
        }
    }

    public interface IMongoCustomerRepository : IMongoRepository<MongoCustomerCollection>
    {
    }
    public class MongoCustomerRepository : MongoRepository<MongoCustomerCollection>, IMongoCustomerRepository
    {
        public MongoCustomerRepository(IOptions<MongoConfiguration> mongoConfig) : base(mongoConfig)
        {
        }
    }
}
