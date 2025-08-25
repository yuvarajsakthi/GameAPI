using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class UserRepository : IUser, IGameApiRepository<User>
    {
        private readonly ILogger<UserRepository> _logger;
        protected readonly GameApiContext _context;
        protected readonly DbSet<User> _dbSet;

        public UserRepository(GameApiContext context, ILogger<UserRepository> logger)
        {
            _logger = logger;
            _context = context;
            _dbSet = _context.Users;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all entities of type {Type}", typeof(User).Name);
                throw new RepositoryException($"Failed to retrieve all {typeof(User).Name} entities", ex);
            }
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting entity of type {Type} with ID {Id}", typeof(User).Name, id);
                throw new RepositoryException($"Failed to retrieve {typeof(User).Name} with ID {id}", ex);
            }
        }

        public async Task<User> AddAsync(User entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await SaveAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding entity of type {Type}", typeof(User).Name);
                throw new RepositoryException($"Failed to add {typeof(User).Name} entity", ex);
            }
        }

        public async Task<User> UpdateAsync(User entity)
        {
            try
            {
                _dbSet.Update(entity);
                await SaveAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating entity of type {Type}", typeof(User).Name);
                throw new RepositoryException($"Failed to update {typeof(User).Name} entity", ex);
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
                _logger.LogError(ex, "Error occurred while deleting entity of type {Type} with ID {Id}", typeof(User).Name, id);
                throw new RepositoryException($"Failed to delete {typeof(User).Name} with ID {id}", ex);
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

        public async Task<IEnumerable<User>> SortAsync<TKey>(Func<User, TKey> keySelector, bool descending = false)
        {
            try
            {
                var sorted = descending ? _dbSet.AsEnumerable().OrderByDescending(keySelector)
                                        : _dbSet.AsEnumerable().OrderBy(keySelector);
                return await Task.FromResult(sorted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sorting entities of type {Type}", typeof(User).Name);
                throw new RepositoryException($"Failed to sort {typeof(User).Name} entities", ex);
            }
        }

        public async Task<int> CountAsync(Func<User, bool>? predicate = null)
        {
            try
            {
                return await Task.FromResult(predicate == null
                    ? _dbSet.Count()
                    : _dbSet.Count(predicate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while counting entities of type {Type}", typeof(User).Name);
                throw new RepositoryException($"Failed to count {typeof(User).Name} entities", ex);
            }
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by email '{Email}'", email);
                throw new RepositoryException($"Failed to retrieve user with email '{email}'", ex);
            }
        }

        public async Task<IEnumerable<User>> GetByRoleAsync(string role)
        {
            try
            {
                return await _dbSet.Where(u => u.Role == role).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting users by role '{Role}'", role);
                throw new RepositoryException($"Failed to retrieve users with role '{role}'", ex);
            }
        }
    }
}