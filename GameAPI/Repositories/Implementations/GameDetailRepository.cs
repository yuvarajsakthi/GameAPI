using GameAPI.Data;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Repositories.Implementations
{
    public class GameDetailRepository(GameApiContext context) : GameApiRepository<GameDetail>(context), IGameDetail
    {
        public async Task<IEnumerable<GameDetail>> GetReleasedAfterAsync(DateTime date)
        {
            return await _dbSet.Where(d => d.ReleaseDate > date).ToListAsync();
        }

        public async Task<IEnumerable<GameDetail>> GetByGenreAsync(string genre)
        {
            return await _dbSet.Where(d => d.Genre == genre).ToListAsync();
        }
    }
}
