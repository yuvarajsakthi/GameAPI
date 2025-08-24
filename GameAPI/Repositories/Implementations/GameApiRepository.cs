using GameAPI.Data;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameAPI.Repositories.Implementations
{
    public class GameApiRepository<T> : IGameApiRepository<T> where T : class
    {
        protected readonly GameApiContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly ILogger<GameApiRepository<T>> _logger;

        public GameApiRepository(GameApiContext context, ILogger<GameApiRepository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all entities of type {Type}", typeof(T).Name);
                throw new RepositoryException($"Failed to retrieve all {typeof(T).Name} entities", ex);
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting entity of type {Type} with ID {Id}", typeof(T).Name, id);
                throw new RepositoryException($"Failed to retrieve {typeof(T).Name} with ID {id}", ex);
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await SaveAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding entity of type {Type}", typeof(T).Name);
                throw new RepositoryException($"Failed to add {typeof(T).Name} entity", ex);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await SaveAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating entity of type {Type}", typeof(T).Name);
                throw new RepositoryException($"Failed to update {typeof(T).Name} entity", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) return false;

                _dbSet.Remove(entity);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting entity of type {Type} with ID {Id}", typeof(T).Name, id);
                throw new RepositoryException($"Failed to delete {typeof(T).Name} with ID {id}", ex);
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving changes to database");
                throw new RepositoryException("Failed to save changes to database", ex);
            }
        }

        public async Task<IEnumerable<T>> SortAsync<TKey>(Func<T, TKey> keySelector, bool descending = false)
        {
            try
            {
                var sorted = descending ? _dbSet.AsEnumerable().OrderByDescending(keySelector)
                                        : _dbSet.AsEnumerable().OrderBy(keySelector);
                return await Task.FromResult(sorted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sorting entities of type {Type}", typeof(T).Name);
                throw new RepositoryException($"Failed to sort {typeof(T).Name} entities", ex);
            }
        }

        public async Task<int> CountAsync(Func<T, bool>? predicate = null)
        {
            try
            {
                return await Task.FromResult(predicate == null
                    ? _dbSet.Count()
                    : _dbSet.Count(predicate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while counting entities of type {Type}", typeof(T).Name);
                throw new RepositoryException($"Failed to count {typeof(T).Name} entities", ex);
            }
        }
    }

    // Custom exception class for repository errors
    public class RepositoryException : Exception
    {
        public RepositoryException() { }
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(string message, Exception innerException) : base(message, innerException) { }
    }
}