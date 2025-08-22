using GameAPI.Models;
using GameAPI.Repositories.Interfaces;

namespace GameAPI.Services
{
    public class GameService : BaseService<Game>
    {
        private readonly IGame _gameRepo;

        public GameService(IGame gameRepo) : base(gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public async Task<IEnumerable<Game>> GetByCompanyAsync(int companyId) => await _gameRepo.GetByCompanyAsync(companyId);

        public async Task<IEnumerable<Game>> GetByPlatformAsync(int platformId) => await _gameRepo.GetByPlatformAsync(platformId);

        public async Task<Game> AddGameAsync(Game game) => await _gameRepo.AddAsync(game);

        public async Task<Game> UpdateGameAsync(Game game) => await _gameRepo.UpdateAsync(game);

        public async Task<bool> DeleteGameAsync(int gameId) => await _gameRepo.DeleteAsync(gameId);
    }
}
