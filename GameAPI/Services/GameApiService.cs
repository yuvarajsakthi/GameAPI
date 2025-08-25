using AutoMapper;
using GameAPI.Repositories.Interfaces;
using System.Linq.Expressions;

namespace GameAPI.Services
{
    public class GameApiService<T> where T : class
    {
        protected readonly IGameApiRepository<T> _repository;
        protected readonly IMapper _mapper;
        public GameApiService(IGameApiRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<T?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task<T> AddAsync(T entity) => await _repository.AddAsync(entity);
        public async Task<T> UpdateAsync(T entity) => await _repository.UpdateAsync(entity);
        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
        public async Task<IEnumerable<T>> SortAsync<TKey>(Func<T, TKey> keySelector, bool descending = false) => await _repository.SortAsync(keySelector, descending);
        public async Task<int> CountAsync(Func<T, bool>? predicate = null) => await _repository.CountAsync(predicate);

        public async Task<T> AddFromDtoAsync<TDto>(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            return await _repository.AddAsync(entity);
        }

    }
}
