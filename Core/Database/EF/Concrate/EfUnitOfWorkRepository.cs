using Core.Database.EF.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.EF.Concrate
{
    public class EfUnitOfWorkRepository<TContext,TEntity> : IEfUnitOfWorkRepository<TEntity> where TEntity : class, IEntity where TContext :DbContext
    {
        protected readonly TContext Context;
        public EfUnitOfWorkRepository(TContext context)
        {
            Context = context;  
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
           var result=  await Context.Set<TEntity>().AddAsync(entity);
           return result.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate =null)
        {
            if (predicate==null)
            {
                return Context.Set<TEntity>().FirstOrDefaultAsync();
            }
            return Context.Set<TEntity>().Where(predicate)?.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate=null)
        {
            if (predicate == null)
            {
                return await Context.Set<TEntity>().ToListAsync();
            }
            return await Context.Set<TEntity>().Where(predicate)?.ToListAsync();
        }

        public Task RemoveAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
