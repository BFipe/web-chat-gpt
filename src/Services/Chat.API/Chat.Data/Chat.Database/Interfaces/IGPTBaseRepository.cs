using Chat.Entities.EntityDepenceInterfaces;
using System.Linq.Expressions;

namespace Chat.Database.Interfaces
{
    public interface IGPTBaseRepository<T> where T : class, IDatabaseStorable
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<bool> IsEntityExists(string id);
        Task<bool> IsEntitiesExists(Expression<Func<T, bool>> predicate);
    }
}