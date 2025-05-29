using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Common
{
    public class Game : Item
    {
        public string Genre { get; set; }
        public int HoursToComplete { get; set; }
        public double CriticsRating { get; set; }
        public static string Platform { get; set; } // Статичне поле
        public static List<Game> Games { get; set; } = new List<Game>(); // Колекція ігор

        // статичний конструктор
        static Game()
        {
            Platform = "PC";
        }

        // статичний метод
        public static string GetPlatformInfo()
        {
            return $"This game is available on {Platform}";
        }

        public Game(string title, string developer, string genre, int hoursToComplete, double criticsRating)
        {
            Id = Guid.NewGuid();
            Title = title;
            Developer = developer;
            Genre = genre;
            HoursToComplete = hoursToComplete;
            CriticsRating = criticsRating;
        }

        // конструктор за замовчуванням
        public Game()
        {
            Id = Guid.NewGuid();
            Title = "Unknown Game";
            Developer = "Indie Dev";
            Genre = "Undefined";
            HoursToComplete = 0;
            CriticsRating = 0.0;
        }

        // статичний метод для створення нового об'єкта Game
        public static Game CreateNew()
        {
            var random = new Random();
            string[] genres = { "RPG", "Action", "Strategy", "Adventure", "Shooter" };
            string[] titles = { "CyberQuest", "Shadow Wars", "Pixel Kingdom", "Galactic Strikers", "Dungeon Escape" };
            string[] developers = { "Ubisoft", "EA", "Nintendo", "Bethesda", "Indie Studio" };

            var newGame = new Game(
                titles[random.Next(titles.Length)],
                developers[random.Next(developers.Length)],
                genres[random.Next(genres.Length)],
                random.Next(10, 200), // години проходження
                Math.Round(random.NextDouble() * 10, 1) // рейтинг 0-10
            );

            Games.Add(newGame);
            return newGame;
        }

        // статичний метод для аналізу даних
        public static void ShowStatistics()
        {
            if (Games == null || Games.Count == 0)
            {
                Console.WriteLine("Немає доступних ігор для аналізу.");
                return;
            }

            var minRating = Games.Min(game => game?.CriticsRating ?? 0);
            var maxRating = Games.Max(game => game?.CriticsRating ?? 0);
            var avgRating = Games.Average(game => game?.CriticsRating ?? 0);

            var mostPopularGenre = Games
                .Where(game => game != null)
                .GroupBy(game => game.Genre)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "Unknown";

            Console.WriteLine($"min rating: {minRating}");
            Console.WriteLine($"max rating: {maxRating}");
            Console.WriteLine($"avg rating: {avgRating}");
            Console.WriteLine($"most popuar genre: {mostPopularGenre}");
        }
    }
}
