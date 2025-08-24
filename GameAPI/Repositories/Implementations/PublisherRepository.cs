using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class PublisherRepository : GameApiRepository<Publisher>, IPublisher
    {
        private readonly ILogger<PublisherRepository> _logger;

        public PublisherRepository(GameApiContext context, ILogger<PublisherRepository> logger)
            : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Publisher>> GetByCountryAsync(string country)
        {
            try
            {
                return await _dbSet.Where(p => p.Country == country).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting publishers by country '{Country}'", country);
                throw new RepositoryException($"Failed to retrieve publishers from country '{country}'", ex);
            }
        }

        public async Task<Publisher?> GetByNameAsync(string name)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(p => p.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting publisher by name '{Name}'", name);
                throw new RepositoryException($"Failed to retrieve publisher with name '{name}'", ex);
            }
        }
    }
}