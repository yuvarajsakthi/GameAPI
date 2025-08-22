using GameAPI.Models;
using GameAPI.Repositories.Interfaces;

namespace GameAPI.Services
{
    public class PlatformService : BaseService<Platform>
    {
        private readonly IPlatform _repo;

        public PlatformService(IPlatform repo) : base(repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Platform>> GetByTypeAsync(string type) => await _repo.GetByTypeAsync(type);
        public async Task<Platform?> GetByNameAsync(string name) => await _repo.GetByNameAsync(name);
    }
}
