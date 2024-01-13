using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Mongo.Abstract
{
    public interface IMongoRepository<TCollection> where TCollection : class,IAknMongoCollection
    {
        Task<List<TCollection>> GetListAsync(Expression<Func<TCollection, bool>> predicate = null);
        Task<TCollection> GetAsync(Expression<Func<TCollection, bool>> predicate);
        Task<TCollection> GetByIdAsync(string id);
        Task<TCollection> AddAsync(TCollection entity);
        Task<bool> AddRangeAsync(IEnumerable<TCollection> entities);
        Task<TCollection> UpdateAsync(TCollection entity, Expression<Func<TCollection, bool>> predicate);
        Task<TCollection> DeleteAsync(TCollection entity);
        Task<TCollection> DeleteAsync(string id);
        Task<TCollection> DeleteAsync(Expression<Func<TCollection, bool>> predicate);
    }
}
