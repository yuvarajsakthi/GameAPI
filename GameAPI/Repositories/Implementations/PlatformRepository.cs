using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class PlatformRepository : GameApiRepository<Platform>, IPlatform
    {
        private readonly ILogger<PlatformRepository> _logger;

        public PlatformRepository(GameApiContext context, ILogger<PlatformRepository> logger)
            : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Platform>> GetByTypeAsync(string type)
        {
            try
            {
                return await _dbSet.Where(p => p.Type == type).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting platforms by type '{Type}'", type);
                throw new RepositoryException($"Failed to retrieve platforms of type '{type}'", ex);
            }
        }

        public async Task<Platform?> GetByNameAsync(string name)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(p => p.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting platform by name '{Name}'", name);
                throw new RepositoryException($"Failed to retrieve platform with name '{name}'", ex);
            }
        }
    }
}