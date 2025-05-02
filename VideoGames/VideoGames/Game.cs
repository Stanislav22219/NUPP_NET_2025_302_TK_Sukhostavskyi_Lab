using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames
{
    public class Game : Item
    {
        public string Genre { get; set; }
        public int HoursToComplete { get; set; }
        public double CriticsRating { get; set; }

        public static string Platform { get; set; } // Статичне поле

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
    }
}
