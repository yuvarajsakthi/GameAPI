using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class UserRepository : GameApiRepository<User>, IUser
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(GameApiContext context, ILogger<UserRepository> logger)
            : base(context, logger)
        {
            _logger = logger;
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