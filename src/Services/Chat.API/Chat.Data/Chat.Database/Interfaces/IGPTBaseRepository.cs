using Chat.Entities.EntityDepenceInterfaces;
using System.Linq.Expressions;

namespace Chat.Database.Interfaces
{
    public interface IGPTBaseRepository<T> where T : class, IDatabaseStorable
    {
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(string id);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(string id);
        Task<bool> IsEntitiesExists(Expression<Func<T, bool>> predicate);
        Task<bool> IsEntityExists(string id);
        Task<T> UpdateAsync(T entity);
    }
}