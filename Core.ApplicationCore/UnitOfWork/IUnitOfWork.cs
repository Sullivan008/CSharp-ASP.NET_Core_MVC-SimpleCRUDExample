using Core.ApplicationCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Data.DataAccessLayer.Entities.Core;

namespace Core.ApplicationCore.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        Task<int> Commit();

        void Rollback();
    }
}