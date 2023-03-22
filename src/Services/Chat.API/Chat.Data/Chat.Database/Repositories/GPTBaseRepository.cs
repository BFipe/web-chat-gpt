using Chat.Entities.EntityDepenceInterfaces;
using Chat.Exceptions.DatabaseExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Database.Repositories
{
    public class GPTBaseRepository<T> where T : class, IDatabaseStorable
    {
        private readonly ChatDbContext _dbContext;
        private readonly ILogger<GPTBaseRepository<T>> _logger;

        public GPTBaseRepository(ChatDbContext dbContext, ILogger<GPTBaseRepository<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            await ThrowIfNotFound(id);

            _logger.LogInformation($"Returned entity {nameof(T)} with id = {id}");

            return await _dbContext.Set<T>().FirstAsync(q => q.Id == id);
        }

        public async Task<List<T>> GetByExpressionAsync(Expression<Func<T, bool>> predicate)
        {
            _logger.LogInformation($"Returned entities {nameof(T)} using Expression {predicate.ToString()}");

            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await ThrowIfNotFound(entity.Id);

            _logger.LogInformation($"Attempt to update entity {nameof(T)} with id = {entity.Id}");

            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Sucessfully updated entity {nameof(T)} with id = {entity.Id}");

            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            _logger.LogInformation($"Attempt to create new entity {nameof(T)} with id = {entity.Id}");

            await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Sucessfully created new entity {nameof(T)} with id = {entity.Id}");

            return entity;
        }

        public async Task DeleteByIdAsync(string id)
        {
            _logger.LogInformation($"Attempt to delete entity {nameof(T)} with id = {id}");

            _dbContext.Set<T>().Remove(await GetByIdAsync(id));

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Sucessfully deleted entity {nameof(T)} with id = {id}");
        }

        public async Task<bool> IsEntityExists(string id)
        {
            return await _dbContext.Set<T>().AnyAsync(q => q.Id == id);
        }

        private async Task ThrowIfNotFound(string id)
        {
            if (await IsEntityExists(id) == false)
            {
                _logger.LogWarning($"Entity with Id {id} is not found in the database");
                throw new EntityNotFoundException(id);
            }
        }
    }
}
