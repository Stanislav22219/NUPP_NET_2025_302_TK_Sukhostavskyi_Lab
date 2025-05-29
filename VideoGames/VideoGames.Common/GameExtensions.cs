using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Common
{
    public static class GameExtensions
    {
        // метод розширення для класу Game
        public static string GetShortTitle(this Game game)
        {
            // якщо довжина назви більше 10 символів, скоротити її
            return game.Title.Length > 10 ? game.Title.Substring(0, 10) + "..." : game.Title;
        }
    }
}
