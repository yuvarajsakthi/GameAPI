using GameAPI.Models;
using GameAPI.Repositories.Interfaces;

namespace GameAPI.Services
{
    public class PublisherService : BaseService<Publisher>
    {
        private readonly IPublisher _repo;

        public PublisherService(IPublisher repo) : base(repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Publisher>> GetByCountryAsync(string country) => await _repo.GetByCountryAsync(country);
        public async Task<Publisher?> GetByNameAsync(string name) => await _repo.GetByNameAsync(name);
    }
}
