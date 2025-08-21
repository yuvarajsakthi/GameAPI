using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class PlatformRepository(GameApiContext context) : GameApiRepository<Platform>(context), IPlatform
    {
        public async Task<IEnumerable<Platform>> GetByTypeAsync(string type)
        {
            return await _dbSet.Where(p => p.Type == type).ToListAsync();
        }

        public async Task<Platform?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
