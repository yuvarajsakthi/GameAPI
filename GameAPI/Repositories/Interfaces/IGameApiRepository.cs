using GameAPI.Models;

namespace GameAPI.Repositories.Interfaces
{
    public interface IGameApiRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveAsync();

        Task<IEnumerable<T>> SortAsync<TKey>(Func<T, TKey> keySelector, bool descending = false);
        Task<int> CountAsync(Func<T, bool>? predicate = null);
    }

    public interface IUser
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetByRoleAsync(string role);
    }

}
