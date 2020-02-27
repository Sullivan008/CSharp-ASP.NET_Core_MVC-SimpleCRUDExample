using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.DataAccessLayer.Entities.Core;

namespace Core.ApplicationCore.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Query();

        Task<ICollection<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);

        IEnumerable<TEntity> Filter(
            Expression<Func<TEntity, bool>> filterPredicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByPredicate = null,
            string navigationProperties = "",
            int? page = null,
            int? pageSize = null);
    }
}
