using Core.Database.Mongo.Abstract;
using Core.Exception;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Mongo.Concrate
{
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        private readonly IOptions<MongoConfiguration> _mongoConfig;
        private readonly IMongoSessionFactory _mongoSessionFactory;
        private readonly IMongoDatabase _mongoDatabase;
        public IClientSessionHandle Session { get; private set; }
        public ConcurrentDictionary<Type, object> Repositorys { get; set; } = new ConcurrentDictionary<Type, object>();
        public MongoUnitOfWork(IOptions<MongoConfiguration> mongoConfig, IMongoSessionFactory mongoSessionFactory, IMongoDatabase mongoDatabase)
        {
            _mongoConfig = mongoConfig;
            _mongoSessionFactory = mongoSessionFactory;
            _mongoDatabase = mongoDatabase;

        }
        public Task CommitTransactionAsync()
        {
            return Session.CommitTransactionAsync();
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<IMongoUnitOfWork, Task<TResult>> func)
        {
            try
            {
                await StartTransactionAsync();
                var result = await func(this);
                await CommitTransactionAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                await RollBackTransactionAsync();
                var aknException = new AknException(ex);
                throw aknException;
            }
        }

        public Task RollBackTransactionAsync()
        {
            return Session.AbortTransactionAsync();
        }

        public async Task StartTransactionAsync()
        {
            Session = await _mongoSessionFactory.CreateTaskAsync();
            Session.StartTransaction();
        }

        public IMongoRepository<TCollection> GetRepository<TCollection>() where TCollection : class, IAknMongoCollection
        {
            if (Repositorys.ContainsKey(typeof(TCollection)))
            {
                Repositorys.TryGetValue(typeof(TCollection), out var repository);
                return (IMongoRepository<TCollection>)repository;
            }
            var result = new MongoRepository<TCollection>(_mongoConfig, _mongoDatabase, Session);
            Repositorys.TryAdd(typeof(TCollection), result);
            return result;
        }

        public void Dispose()
        {
            if (Session !=null)
            {
                Session.Dispose();               
            }
        }
    }
}
