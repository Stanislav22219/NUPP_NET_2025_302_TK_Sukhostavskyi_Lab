using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace VideoGames.Common
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly List<T> _data = new();
        private readonly Func<T, Guid> _idSelector;
        private readonly SemaphoreSlim _semaphore = new(1, 1); // для контролю доступу
        private static readonly object _lock = new object(); // Lock для потокобезпеки
        private static readonly AutoResetEvent _resetEvent = new AutoResetEvent(true);
        private static readonly Mutex _mutex = new Mutex();

        public CrudServiceAsync(Func<T, Guid> idSelector)
        {
            _idSelector = idSelector;
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _semaphore.WaitAsync();
            try
            {
                lock (_lock) // Захист списку
                {
                    _data.Add(element);
                }
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<T> ReadAsync(Guid id)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _data.FirstOrDefault(x => _idSelector(x) == id);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                return _data.ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _data.Skip((page - 1) * amount).Take(amount).ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> UpdateAsync(T element)
        {
            await _semaphore.WaitAsync();
            try
            {
                _mutex.WaitOne(); // Захист від одночасного оновлення
                var id = _idSelector(element);
                var existingElement = await ReadAsync(id);
                if (existingElement != null)
                {
                    _data.Remove(existingElement);
                    _data.Add(element);
                    return true;
                }
                return false;
            }
            finally
            {
                _mutex.ReleaseMutex();
                _semaphore.Release();
            }
        }

        public async Task<bool> RemoveAsync(T element)
        {
            await _semaphore.WaitAsync();
            try
            {
                _resetEvent.WaitOne(); // Чекає сигнал
                bool result;
                lock (_lock)
                {
                    result = _data.Remove(element);
                }
                _resetEvent.Set(); // Дозволяє наступному потоку виконання
                return result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> SaveAsync(string FilePath)
        {
            await _semaphore.WaitAsync();
            try
            {
                var jsonData = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(FilePath, jsonData);
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public IEnumerator<T> GetEnumerator() => _data.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
