using Core.Database.EF.Abstract;
using Core.Database.UnitofWork.Abstract;
using Core.Exception;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.UnitofWork.Concrate
{
    public class EfUnitOfWork : IEfUnitofWork
    {
        protected readonly DbContext Context;
        public ConcurrentDictionary<Type, IEfUnitOfWorkRepository<IEntity>> Repositorys  { get; set; } 
        public EfUnitOfWork(DbContext context)
        {
            this.Context = context;
        }
        public Task CommitTransactionAsync()
        {
            return Context.Database.CommitTransactionAsync();
        }

        public async Task<TResult> ExecuteAsync<TInput, TResult>(Func<TInput,Task<TResult>> func) where TInput : class,new()
        {
            try
            {
                await StartTransactionAsync();
                var result = await func(new TInput());
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

        public IEfUnitOfWorkRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            return null;
            //return Repositorys.GetOrAdd(typeof(TEntity), null);
        }

        public Task RollBackTransactionAsync()
        {
            return Context.Database.RollbackTransactionAsync();
        }

        public Task StartTransactionAsync()
        {
            return Context.Database.BeginTransactionAsync();
        }
    }
}
