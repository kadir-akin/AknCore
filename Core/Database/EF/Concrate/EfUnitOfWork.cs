using Core.Database.EF.Abstract;
using Core.Exception;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Core.Database.EF.Concrate
{
    public class EfUnitOfWork<TContext> :IEfUnitofWork where TContext : DbContext   
    {
        protected readonly TContext Context;
        public ConcurrentDictionary<Type, object> Repositorys { get; set; } = new ConcurrentDictionary<Type, object>();
        private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction DbContextTransaction;
        public EfUnitOfWork(TContext aknDbContext) 
        {
            this.Context = aknDbContext;
        }
        public async Task CommitTransactionAsync()
        {
             await Context.SaveChangesAsync();
             await DbContextTransaction.CommitAsync();
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                await StartTransactionAsync();
                var result = await func();
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
            if (Repositorys.ContainsKey(typeof(TEntity)))
            {
               Repositorys.TryGetValue(typeof(TEntity), out var repository);
               return (IEfUnitOfWorkRepository<TEntity>)repository;
            }
            var result = new EfUnitOfWorkRepository<TContext, TEntity>(Context);
            Repositorys.TryAdd(typeof(TEntity), result);
            return result;      
        }

        public Task RollBackTransactionAsync()
        {
            return DbContextTransaction.RollbackAsync();
        }

        public async Task StartTransactionAsync()
        {
            DbContextTransaction=await Context.Database.BeginTransactionAsync();
        }
    }
}
