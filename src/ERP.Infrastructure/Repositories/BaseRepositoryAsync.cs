using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Models;
using ERP.Domain.Core.Specifications;
using ERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ERP.Domain.Exceptions;

namespace ERP.Infrastructure.Repositories
{
    public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
    {
        protected readonly ERPDbContext _dbContext;

        public BaseRepositoryAsync(ERPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<T>> ListAllAsync(bool allowTracking)
        {
            if (allowTracking)
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IList<T>> ListAsync(ISpecification<T> spec, bool allowTracking)
        {
            if (allowTracking)
            {
                return await ApplySpecification(spec).ToListAsync();
            }
            return await ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec, bool allowTracking)
        {
            if (allowTracking)
            {
                return await ApplySpecification(spec).FirstOrDefaultAsync();
            }
            return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<T> SingleAsync(ISpecification<T> spec, bool allowTracking)
        {
            T item;
            if (allowTracking)
            {
                item = await ApplySpecification(spec).FirstOrDefaultAsync();
            }
            else
            {
                item = await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
            }
            if (item == null)
            {
                throw new RecordNotFoundException(string.Format("{0} Not Found", typeof(T).Name));
            }
            return item;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
