using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Mongo.Abstract
{
    public interface IMongoUnitOfWork :IDisposable
    {
        public IMongoRepository<TCollection> GetRepository<TCollection>() where TCollection : class, IAknMongoCollection;
        public Task StartTransactionAsync();

        public Task RollBackTransactionAsync();
        public Task CommitTransactionAsync();

        public Task<TResult> ExecuteAsync<TResult>(Func<IMongoUnitOfWork, Task<TResult>> func);

       
    }
}
