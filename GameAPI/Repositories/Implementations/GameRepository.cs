using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class GameRepository(GameApiContext context) : GameApiRepository<Game>(context), IGame
    {
        public async Task<IEnumerable<Game>> GetByCompanyAsync(int companyId)
        {
            return await _dbSet.Where(g => g.GameCompanyId == companyId).ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetByPlatformAsync(int platformId)
        {
            return await _dbSet
                .Where(g => g.Platforms!.Any(p => p.PlatformId == platformId))
                .ToListAsync();
        }
    }
}
