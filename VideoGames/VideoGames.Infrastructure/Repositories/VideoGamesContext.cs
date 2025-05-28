using Microsoft.EntityFrameworkCore;
using VideoGames.Infrastructure.Models;

namespace VideoGames.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VideoGamesContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(VideoGamesContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveChangesAsync();
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
