using System.Linq.Expressions;
using ERP.Domain.Core.Models;

namespace ERP.Domain.Core.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
        Task<T> LoadRelatedEntity<T>(T entity, params Expression<Func<T, object>>[] includes) where T : BaseEntity;
    }
}