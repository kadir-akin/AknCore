using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Database.EF.Abstract
{
    public interface IEfRepository<TEntity> where TEntity : class,IEntity
    {
        public  Task AddAsync(TEntity entity);

        public Task AddRangeAsync(IEnumerable<TEntity> entities);

        public IEnumerable<TEntity> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        public TEntity GetByIdAsync(int id);

        public Task RemoveAsync(TEntity entity);

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
       
    }
}
