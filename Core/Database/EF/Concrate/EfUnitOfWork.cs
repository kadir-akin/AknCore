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
        private readonly IServiceProvider _serviceProvider;
        public ConcurrentDictionary<Type, object> Repositorys { get; set; } = new ConcurrentDictionary<Type, object>();
        public EfUnitOfWork(TContext aknDbContext, IServiceProvider serviceProvider) 
        {
            this.Context = aknDbContext;
            _serviceProvider = serviceProvider;
        }
        public Task CommitTransactionAsync()
        {
            return Context.Database.CommitTransactionAsync();
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
            var result = _serviceProvider.GetService(typeof(IEfUnitOfWorkRepository<TEntity>));
            Repositorys.TryAdd(typeof(TEntity), result);
            return (IEfUnitOfWorkRepository<TEntity>)result;      
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
