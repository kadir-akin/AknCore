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
    public class MongoRepository<TCollection> : IMongoRepository<TCollection> where TCollection : class, IAknMongoCollection
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
        public async Task<TCollection> AddAsync(TCollection entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await Collection.InsertOneAsync(entity, options);
            return entity;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TCollection> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await Collection.BulkWriteAsync((IEnumerable<WriteModel<TCollection>>)entities, options)).IsAcknowledged;
        }

        public Task<TCollection> DeleteAsync(TCollection entity)
        {
            return  Collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }

        public Task<TCollection> DeleteAsync(string id)
        {
            return  Collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public Task<TCollection> DeleteAsync(Expression<Func<TCollection, bool>> predicate)
        {
            return  Collection.FindOneAndDeleteAsync(predicate);
        }

        public IQueryable<TCollection> Get(Expression<Func<TCollection, bool>> predicate = null)
        {
            return predicate == null
                 ? Collection.AsQueryable()
                 : Collection.AsQueryable().Where(predicate);
        }

        public Task<TCollection> GetAsync(Expression<Func<TCollection, bool>> predicate)
        {
            return Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public Task<TCollection> GetByIdAsync(string id)
        {
            return Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<TCollection> UpdateAsync(string id, TCollection entity)
        {
            return  Collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
        }

        public Task<TCollection> UpdateAsync(TCollection entity, Expression<Func<TCollection, bool>> predicate)
        {
            return  Collection.FindOneAndReplaceAsync(predicate, entity);
        }
    }
}
