using Core.Database.EF.Concrate;
using System;
using System.Threading.Tasks;

namespace Core.Database.EF.Abstract
{
    public interface IEfUnitofWork 
    {

        public IEfUnitOfWorkRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        public Task StartTransactionAsync();

        public Task RollBackTransactionAsync();
        public Task CommitTransactionAsync();

        public Task<TResult> ExecuteAsync<TInput, TResult>(Func<TInput, Task<TResult>> func) where TInput : class, new();

    }
}
