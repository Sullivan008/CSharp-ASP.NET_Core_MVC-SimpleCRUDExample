using Core.ApplicationCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.DataAccessLayer.Entities.Core;

namespace Core.ApplicationCore.Repository
{
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class, IEntity where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        public Repository(TContext context, IUnitOfWork<TContext> unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);

            await _unitOfWork.Commit();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _unitOfWork.Commit();

            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

            return await _unitOfWork.Commit();
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> filterPredicate = null,
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByPredicate = null,
                                           string navigationProperties = "",
                                           int? page = null,
                                           int? pageSize = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }

            if (orderByPredicate != null)
            {
                query = orderByPredicate(query);
            }

            if (navigationProperties != null)
            {
                foreach (string navigationPropertyPath in navigationProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(navigationPropertyPath);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query
                        .Skip((page.Value - 1) * pageSize.Value)
                        .Take(pageSize.Value);
            }

            return query.ToList();
        }
    }
}
