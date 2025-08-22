using GameAPI.Models;
using GameAPI.Repositories.Interfaces;

namespace GameAPI.Services
{
    public class GameDetailService : BaseService<GameDetail>
    {
        private readonly IGameDetail _repo;

        public GameDetailService(IGameDetail repo) : base(repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<GameDetail>> GetReleasedAfterAsync(DateTime date) => await _repo.GetReleasedAfterAsync(date);

        public async Task<IEnumerable<GameDetail>> GetByGenreAsync(string genre) => await _repo.GetByGenreAsync(genre);
    }
}
