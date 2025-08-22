using GameAPI.Data;
using GameAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameAPI.Repositories.Implementations
{
    public class GameApiRepository<T> : IGameApiRepository<T> where T : class
    {
        protected readonly GameApiContext _context;
        protected readonly DbSet<T> _dbSet;

        public GameApiRepository(GameApiContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<T>> SortAsync<TKey>(Func<T, TKey> keySelector, bool descending = false)
        {
            var sorted = descending ? _dbSet.AsEnumerable().OrderByDescending(keySelector)
                                    : _dbSet.AsEnumerable().OrderBy(keySelector);
            return await Task.FromResult(sorted);
        }

        public async Task<int> CountAsync(Func<T, bool>? predicate = null)
        {
            return await Task.FromResult(predicate == null
                ? _dbSet.Count()
                : _dbSet.Count(predicate));
        }
    }
}
