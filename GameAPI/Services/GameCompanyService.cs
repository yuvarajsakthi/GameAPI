using GameAPI.Models;
using GameAPI.Repositories.Interfaces;

namespace GameAPI.Services
{
    public class GameCompanyService : BaseService<GameCompany>
    {
        private readonly IGameCompany _repo;

        public GameCompanyService(IGameCompany repo) : base(repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<GameCompany>> GetByFoundedYearAsync(int year) => await _repo.GetByFoundedYearAsync(year);
        public async Task<IEnumerable<GameCompany>> SearchByNameAsync(string name) => await _repo.SearchByNameAsync(name);
        public async Task<IEnumerable<GameCompany>> SearchByHeadquartersAsync(string hq) => await _repo.SearchByHeadquarters(hq);
    }
}
