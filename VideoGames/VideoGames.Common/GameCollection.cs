using System.Collections.Generic;

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
}
