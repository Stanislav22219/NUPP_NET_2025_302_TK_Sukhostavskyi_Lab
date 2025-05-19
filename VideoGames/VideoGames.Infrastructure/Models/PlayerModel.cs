using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Infrastructure.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Level { get; set; }
        public int GameModelId { get; set; } // Зовнішній ключ
        public GameModel GameModel { get; set; } // Навігаційна властивість
        public List<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();

    }
}
