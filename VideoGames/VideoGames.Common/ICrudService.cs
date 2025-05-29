using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using VideoGames.Common;

namespace VideoGames.Common
{
    public class CrudService<T> : ICrudInterface<T> where T : class
    {
        private readonly List<T> _data = new List<T>();
        private readonly Func<T, Guid> _idSelector;

        public CrudService(Func<T, Guid> idSelector)
        {
            _idSelector = idSelector;
        }

        public void Create(T element)
        {
            _data.Add(element);
        }

        public T Read(Guid id)
        {
            return _data.FirstOrDefault(x => _idSelector(x) == id);

        }

        public IEnumerable<T> ReadAll()
        {
            return _data;
        }

        public void Update(T element)
        {
            var id = _idSelector(element);
            var existingElement = Read(id);
            if (existingElement != null)
            {
                _data.Remove(existingElement);
                _data.Add(element);
            }
        }

        public void Remove(T element)
        {
            _data.Remove(element);
        }

        //метод для збереження даних у файл
        public void Save(string filePath)
        {
            var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }


        //метод для завантаження даних із файлу
        public void Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                _data.Clear();
                _data.AddRange(JsonSerializer.Deserialize<List<T>>(json));
            }
        }

    }
}