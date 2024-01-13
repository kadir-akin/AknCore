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
        private readonly IMongoDatabase _mongoDatabase;
        protected readonly IMongoCollection<TCollection> Collection;
        public IClientSessionHandle Session { get; set; }
        public MongoRepository(IOptions<MongoConfiguration> mongoConfig,IMongoClient mongoClient)
        {
            _mongoConfig = mongoConfig;
            _mongoDatabase = mongoClient.GetDatabase(_mongoConfig.Value.Database);
            this.Collection = _mongoDatabase.GetCollection<TCollection>(typeof(TCollection).Name.ToLowerInvariant());
        }
        public MongoRepository(IOptions<MongoConfiguration> mongoConfig, IMongoClient mongoClient, IClientSessionHandle session)
        {
            _mongoConfig = mongoConfig;
            _mongoDatabase = mongoClient.GetDatabase(_mongoConfig.Value.Database);
            Session = session;
            this.Collection = _mongoDatabase.GetCollection<TCollection>(typeof(TCollection).Name.ToLowerInvariant());
        }
       
        public async Task<TCollection> AddAsync(TCollection entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            if (Session ==null)
            {
                await Collection.InsertOneAsync(entity, options);
                return entity;
            }

            await Collection.InsertOneAsync(Session,entity, options);
            return entity;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TCollection> entities)
        {
            if (Session==null)
            {
                var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
                return (await Collection.BulkWriteAsync((IEnumerable<WriteModel<TCollection>>)entities, options)).IsAcknowledged;
            }

            var optionsSes = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await Collection.BulkWriteAsync(Session,(IEnumerable<WriteModel<TCollection>>)entities, optionsSes)).IsAcknowledged;

        }

        public Task<TCollection> DeleteAsync(TCollection entity)
        {
            if (Session==null)
                return Collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
            else
                return Collection.FindOneAndDeleteAsync(Session,x => x.Id == entity.Id);

        }

        public Task<TCollection> DeleteAsync(string id)
        {
            if (Session == null)
                return Collection.FindOneAndDeleteAsync(x => x.Id == id);
            else
                return Collection.FindOneAndDeleteAsync(Session, x => x.Id == id);
        }

        public Task<TCollection> DeleteAsync(Expression<Func<TCollection, bool>> predicate)
        {
            if (Session==null)
            {
                return Collection.FindOneAndDeleteAsync(predicate);
            }
            else
            {
                return Collection.FindOneAndDeleteAsync(Session,predicate);
            }
           
        }

        public Task<List<TCollection>> GetListAsync(Expression<Func<TCollection, bool>> predicate = null)
        {
            if (Session==null)
            {
                return predicate == null
                ? Collection.Find(Builders<TCollection>.Filter.Empty).ToListAsync()
                : Collection.Find(predicate).ToListAsync();
            }
            else
            {
                return predicate == null
               ? Collection.Find(Session, Builders<TCollection>.Filter.Empty).ToListAsync()
               : Collection.Find(Session, predicate).ToListAsync();
            }
           
        }

        public Task<TCollection> GetAsync(Expression<Func<TCollection, bool>> predicate)
        {
            if (Session==null)
            {
                return Collection.Find(predicate).FirstOrDefaultAsync();
            }
            else
            {
                return Collection.Find(Session,predicate).FirstOrDefaultAsync();
            }
          
        }

        public Task<TCollection> GetByIdAsync(string id)
        {
            if (Session==null)
            {
                return Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            else
            {
                return Collection.Find(Session,x => x.Id == id).FirstOrDefaultAsync();
            }
           
        }
    
        public Task<TCollection> UpdateAsync(TCollection entity, Expression<Func<TCollection, bool>> predicate)
        {
            if (Session==null)
            {
                return Collection.FindOneAndReplaceAsync( predicate, entity);
            }

            return Collection.FindOneAndReplaceAsync(Session, predicate, entity);

        }
    }
}
