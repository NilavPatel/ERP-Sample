using System.Linq.Expressions;
using ERP.Domain.Core.Models;

namespace ERP.Application.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task RollBackChangesAsync();
        IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
        Task<T> LoadRelatedEntity<T>(T entity, params Expression<Func<T, object>>[] includes) where T : BaseEntity;
    }
}