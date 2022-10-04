using System.Linq.Expressions;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Models;
using ERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ERP.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ERPDbContext _dbContext;
        private IDictionary<Type, dynamic> _repositories;
        private bool _disposed = false;

        public UnitOfWork(ERPDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, dynamic>();
        }

        public IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity
        {
            var entityType = typeof(T);
            if (_repositories.ContainsKey(entityType))
            {
                return _repositories[entityType];
            }

            var repositoryType = typeof(BaseRepositoryAsync<>);
            var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

            if (repository == null)
            {
                throw new NullReferenceException("Repository object is null");
            }

            _repositories.Add(entityType, repository);
            return (IBaseRepositoryAsync<T>)repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task<T> LoadRelatedEntity<T>(T entity, params Expression<Func<T, object>>[] includes) where T : BaseEntity
        {
            var newEntry = _dbContext.Entry(entity);
            foreach (var navProp in includes)
            {
                string propertyName = navProp.GetPropertyAccess().Name;

                if (newEntry.Navigation(propertyName).Metadata.IsCollection)
                {
                    await newEntry.Collection(propertyName).LoadAsync();
                }
                else
                {
                    await newEntry.Reference(propertyName).LoadAsync();
                }
            }
            return newEntry.Entity;
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _dbContext.DisposeAsync();
                _disposed = true;
            }
        }
    }
}