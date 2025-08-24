using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class GameCompanyRepository : GameApiRepository<GameCompany>, IGameCompany
    {
        private readonly ILogger<GameCompanyRepository> _logger;

        public GameCompanyRepository(GameApiContext context, ILogger<GameCompanyRepository> logger)
            : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<GameCompany>> GetByFoundedYearAsync(int year)
        {
            try
            {
                return await _dbSet.Where(c => c.FoundedYear == year).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting game companies by founded year {Year}", year);
                throw new RepositoryException($"Failed to retrieve game companies founded in {year}", ex);
            }
        }

        public async Task<IEnumerable<GameCompany>> SearchByNameAsync(string namePart)
        {
            try
            {
                return await _dbSet
                    .Where(c => c.Name!.Contains(namePart))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching game companies by name part '{NamePart}'", namePart);
                throw new RepositoryException($"Failed to search game companies by name '{namePart}'", ex);
            }
        }

        public async Task<IEnumerable<GameCompany>> SearchByHeadquarters(string headquarters)
        {
            try
            {
                return await _dbSet
                    .Where(c => c.HeadQuarter!.Contains(headquarters))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching game companies by headquarters '{Headquarters}'", headquarters);
                throw new RepositoryException($"Failed to search game companies by headquarters '{headquarters}'", ex);
            }
        }
    }
}