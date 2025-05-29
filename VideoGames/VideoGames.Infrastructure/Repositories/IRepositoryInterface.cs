namespace VideoGames.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id); // Отримати елемент за ID (nullable)
        Task<IEnumerable<T>> GetAllAsync(); // Отримати всі елементи
        Task<bool> AddAsync(T entity); // Додати новий елемент
        Task<bool> UpdateAsync(T entity); // Оновити елемент
        Task<bool> DeleteAsync(T entity); // Видалити елемент
        Task<bool> SaveChangesAsync();
    }

}
