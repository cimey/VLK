using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepositoryBase<T, TKey> where T : class where TKey : struct
    {
        Task<T?> GetByIdAsync(TKey id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}