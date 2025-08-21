using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class GameCompanyRepository(GameApiContext context) : GameApiRepository<GameCompany>(context), IGameCompany
    {
        public async Task<IEnumerable<GameCompany>> GetByFoundedYearAsync(int year)
        {
            return await _dbSet.Where(c => c.FoundedYear == year).ToListAsync();
        }

        public async Task<IEnumerable<GameCompany>> SearchByNameAsync(string namePart)
        {
            return await _dbSet
                .Where(c => c.Name!.Contains(namePart))
                .ToListAsync();
        }

        public async Task<IEnumerable<GameCompany>> SearchByHeadquarters(string headquarters)
        {
            return await _dbSet
                .Where(c => c.HeadQuarter!.Contains(headquarters))
                .ToListAsync();
        }
    }
}
