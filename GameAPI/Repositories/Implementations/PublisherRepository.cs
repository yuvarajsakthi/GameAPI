using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class PublisherRepository(GameApiContext context) : GameApiRepository<Publisher>(context), IPublisher
    {
        public async Task<IEnumerable<Publisher>> GetByCountryAsync(string country)
        {
            return await _dbSet.Where(p => p.Country == country).ToListAsync();
        }

        public async Task<Publisher?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
