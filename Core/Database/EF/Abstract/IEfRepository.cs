﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Database.EF.Abstract
{
    public interface IEfRepository<TEntity> where TEntity : class,IEntity
    {
        public Task<TEntity> AddAsync(TEntity entity);
        public Task AddRangeAsync(IEnumerable<TEntity> entities);
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null);
        public Task<TEntity> GetByIdAsync(int id);
        public Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null);
        public Task RemoveAsync(TEntity entity);
        public Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        

    }
}
