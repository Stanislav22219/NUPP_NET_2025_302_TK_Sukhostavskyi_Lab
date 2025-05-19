using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Infrastructure.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public double CriticsRating { get; set; }

        // Зв’язок один-до-одного (гра має одного розробника)
        public DeveloperModel Developer { get; set; }

        // Зв’язок один-до-багатьох (гра може мати багато гравців)
        public List<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();
        public List<PlayerModel> Players { get; set; } = new List<PlayerModel>();
    }
}
