using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class GameDetailRepository : GameApiRepository<GameDetail>, IGameDetail
    {
        private readonly ILogger<GameDetailRepository> _logger;

        public GameDetailRepository(GameApiContext context, ILogger<GameDetailRepository> logger)
            : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<GameDetail>> GetReleasedAfterAsync(DateTime date)
        {
            try
            {
                return await _dbSet.Where(d => d.ReleaseDate > date).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting game details released after {Date}", date);
                throw new RepositoryException($"Failed to retrieve game details released after {date:yyyy-MM-dd}", ex);
            }
        }

        public async Task<IEnumerable<GameDetail>> GetByGenreAsync(string genre)
        {
            try
            {
                return await _dbSet.Where(d => d.Genre == genre).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting game details by genre '{Genre}'", genre);
                throw new RepositoryException($"Failed to retrieve game details with genre '{genre}'", ex);
            }
        }
    }
}