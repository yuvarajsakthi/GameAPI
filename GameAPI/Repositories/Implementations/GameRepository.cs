using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class GameRepository : GameApiRepository<Game>, IGame
    {
        private readonly ILogger<GameRepository> _logger;

        public GameRepository(GameApiContext context, ILogger<GameRepository> logger)
            : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Game>> GetByCompanyAsync(int companyId)
        {
            try
            {
                return await _dbSet.Where(g => g.GameCompanyId == companyId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting games by company ID {CompanyId}", companyId);
                throw new RepositoryException($"Failed to retrieve games for company ID {companyId}", ex);
            }
        }

        public async Task<IEnumerable<Game>> GetByPlatformAsync(int platformId)
        {
            try
            {
                return await _dbSet
                    .Where(g => g.Platforms!.Any(p => p.PlatformId == platformId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting games by platform ID {PlatformId}", platformId);
                throw new RepositoryException($"Failed to retrieve games for platform ID {platformId}", ex);
            }
        }
    }
}