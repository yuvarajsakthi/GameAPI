using AutoMapper;
using GameAPI.DTOs;
using GameAPI.Models;
using GameAPI.Repositories.Implementations;
using GameAPI.Repositories.Interfaces;
using NuGet.Protocol.Core.Types;

namespace GameAPI.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepo;
        protected readonly IMapper _mapper;

        public UserService(UserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepo.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await _userRepo.GetByRoleAsync(role);
        }

        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            if (await _userRepo.GetByEmailAsync(userDto.Email!) != null)
            {
                throw new InvalidOperationException($"User with email '{userDto.Email}' already exists.");
            }

            var newUser = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role
            };

            return await _userRepo.AddAsync(newUser);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _userRepo.GetByIdAsync(user.UserId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {user.UserId} not found.");
            }

            return await _userRepo.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return await _userRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> SortUsersAsync<TKey>(Func<User, TKey> keySelector, bool descending = false)
        {
            return await _userRepo.SortAsync(keySelector, descending);
        }

        public async Task<int> GetUsersCountAsync(Func<User, bool>? predicate = null)
        {
            return await _userRepo.CountAsync(predicate);
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id) != null;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepo.GetByEmailAsync(email) != null;
        }

        public async Task<User> AddFromDtoAsync<TDto>(TDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            return await _userRepo.AddAsync(entity);
        }

    }
}
