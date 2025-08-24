using GameAPI.Models;
using GameAPI.Repositories.Interfaces;

namespace GameAPI.Services
{
    public class UserService : BaseService<User>
    {
        private readonly IUser _userRepo;

        public UserService(IUser userRepo) : base(userRepo) 
        {
            _userRepo = userRepo;
        }

        public async Task<User?> GetByEmailAsync(string email) => await _userRepo.GetByEmailAsync(email);

        public async Task<IEnumerable<User>> GetByRoleAsync(string role) => await _userRepo.GetByRoleAsync(role);

    }
}
