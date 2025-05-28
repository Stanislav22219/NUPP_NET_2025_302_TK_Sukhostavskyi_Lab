using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGames.Infrastructure.Repositories;

namespace VideoGames.Common
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public CrudServiceAsync(IRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> CreateAsync(T element) => await _repository.AddAsync(element);
        public async Task<T?> ReadAsync(Guid id) => await _repository.GetByIdAsync(id);
        public async Task<IEnumerable<T>> ReadAllAsync() => await _repository.GetAllAsync();
        public async Task<bool> UpdateAsync(T element) => await _repository.UpdateAsync(element);
        public async Task<bool> RemoveAsync(T element) => await _repository.DeleteAsync(element);
        public async Task<bool> SaveAsync() => await _repository.SaveChangesAsync();
        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            var items = await _repository.GetAllAsync();
            return items.Skip((page - 1) * amount).Take(amount);
        }


    }
}
