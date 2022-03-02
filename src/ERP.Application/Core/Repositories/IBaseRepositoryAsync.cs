using ERP.Domain.Core.Models;
using ERP.Domain.Core.Specifications;

namespace ERP.Application.Core.Repositories
{
    public interface IBaseRepositoryAsync<T> where T : BaseEntity
    {
        //Task<T> GetByIdAsync(Guid id, bool allowTracking);
        Task<IList<T>> ListAllAsync(bool allowTracking);
        Task<IList<T>> ListAsync(ISpecification<T> spec, bool allowTracking);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec, bool allowTracking);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
