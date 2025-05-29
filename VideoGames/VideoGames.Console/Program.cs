using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGames.Common;

namespace VideoGames.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var gameService = new CrudServiceAsync<Game>(game => game.Id);
            var games = new List<Game>();
            string filePath = "games.json";

            // Використання Parallel.For для масового створення об'єктів
            Parallel.For(0, 1000, _ =>
            {
                var newGame = Game.CreateNew();
                lock (games) // Захист списку від конкурентного доступу
                {
                    games.Add(newGame);
                }
            });

            // Додавання всіх створених об'єктів у сервіс
            foreach (var game in games)
            {
                await gameService.CreateAsync(game);
            }

            // Аналіз даних
            Game.ShowStatistics();

            // Збереження у файл
            await gameService.SaveAsync(filePath);
            System.Console.WriteLine($"Data saved in: {filePath}");
        }
    }
}
