using GameAPI.Repositories.Interfaces;
using System.Linq.Expressions;

namespace GameAPI.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly IGameApiRepository<T> _repository;

        public BaseService(IGameApiRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync();
        public virtual async Task<T?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public virtual async Task<T> AddAsync(T entity) => await _repository.AddAsync(entity);
        public virtual async Task<T> UpdateAsync(T entity) => await _repository.UpdateAsync(entity);
        public virtual async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
        public virtual async Task<IEnumerable<T>> SortAsync<TKey>(Func<T, TKey> keySelector, bool descending = false) => await _repository.SortAsync(keySelector, descending);
        public virtual async Task<int> CountAsync(Func<T, bool>? predicate = null) => await _repository.CountAsync(predicate);
    }
}
