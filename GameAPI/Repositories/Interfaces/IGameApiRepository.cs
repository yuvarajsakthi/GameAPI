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

    public interface IUser : IGameApiRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetByRoleAsync(string role);
    }

    public interface IGame : IGameApiRepository<Game>
    {
        Task<IEnumerable<Game>> GetByCompanyAsync(int companyId);
        Task<IEnumerable<Game>> GetByPlatformAsync(int platformId);
    }

    public interface IGameCompany : IGameApiRepository<GameCompany>
    {
        Task<IEnumerable<GameCompany>> GetByFoundedYearAsync(int year);
        Task<IEnumerable<GameCompany>> SearchByNameAsync(string namePart);
        Task<IEnumerable<GameCompany>> SearchByHeadquarters(string headquarters);
    }

    public interface IPublisher : IGameApiRepository<Publisher>
    {
        Task<IEnumerable<Publisher>> GetByCountryAsync(string country);
        Task<Publisher?> GetByNameAsync(string name);
    }

    public interface IPlatform : IGameApiRepository<Platform>
    {
        Task<IEnumerable<Platform>> GetByTypeAsync(string type);
        Task<Platform?> GetByNameAsync(string name);
    }

    public interface IGameDetail : IGameApiRepository<GameDetail>
    {
        Task<IEnumerable<GameDetail>> GetReleasedAfterAsync(DateTime date);
        Task<IEnumerable<GameDetail>> GetByGenreAsync(string genre);
    }
}
