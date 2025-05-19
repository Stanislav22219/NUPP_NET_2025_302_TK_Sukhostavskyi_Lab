using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Infrastructure.Models
{
    public class GamePlayer
    {
        public int GameId { get; set; }
        public GameModel Game { get; set; }

        public int PlayerId { get; set; }
        public PlayerModel Player { get; set; }
    }
}
