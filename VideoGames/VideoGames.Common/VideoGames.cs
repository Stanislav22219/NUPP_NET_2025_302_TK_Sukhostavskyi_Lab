namespace VideoGames.Common
{
    public class GameCollection
    {
        private List<Game> games = new List<Game>();

        // делегат і подія
        public delegate void GameAddedHandler(string message);
        public event GameAddedHandler GameAdded;

        // метод додавання гри
        public void AddGame(Game game)
        {
            games.Add(game);

            // виклик події
            GameAdded?.Invoke($"Game \"{game.Title}\" added!");
        }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }

        public string GetDescription()
        {
            return $"{Title} developed by {Developer}";
        }
    }

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

    public static class GameExtensions
    {
        // метод розширення для класу Game
        public static string GetShortTitle(this Game game)
        {
            // якщо довжина назви більше 10 символів, скоротити її
            return game.Title.Length > 10 ? game.Title.Substring(0, 10) + "..." : game.Title;
        }
    }

    public class Magazine : Item
    {
        public string Publisher { get; set; }
        public int IssueNumber { get; set; }
        public double CriticsRating { get; set; }
    }

    public class Gamer
    {
        public Guid GamerId { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
    }
}
