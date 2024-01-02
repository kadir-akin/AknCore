using Core.Database.Mongo.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Mongo.Concrate
{
    public class MongoRepository<TCollection> : IMongoRepository<TCollection> where TCollection : class, IMongoCollection
    {
        private readonly IOptions<MongoConfiguration> _mongoConfig;
        protected readonly IMongoCollection<TCollection> Collection;
        public MongoRepository(IOptions<MongoConfiguration> mongoConfig)
        {
            _mongoConfig = mongoConfig;
            var client = new MongoClient($"mongodb://{_mongoConfig.Value.UserName}:{_mongoConfig.Value.Password}@{_mongoConfig.Value.Server}");            
            var db = client.GetDatabase(_mongoConfig.Value.Database);
            this.Collection = db.GetCollection<TCollection>(typeof(TCollection).Name.ToLowerInvariant());
        }
        public Task<TCollection> AddAsync(TCollection entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRangeAsync(IEnumerable<TCollection> entities)
        {
            throw new NotImplementedException();
        }

        public Task<TCollection> DeleteAsync(TCollection entity)
        {
            throw new NotImplementedException();
        }

        public Task<TCollection> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TCollection> DeleteAsync(Expression<Func<TCollection, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TCollection> Get(Expression<Func<TCollection, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<TCollection> GetAsync(Expression<Func<TCollection, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TCollection> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TCollection> UpdateAsync(string id, TCollection entity)
        {
            throw new NotImplementedException();
        }

        public Task<TCollection> UpdateAsync(TCollection entity, Expression<Func<TCollection, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
