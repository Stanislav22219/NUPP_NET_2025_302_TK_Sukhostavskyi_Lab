using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Infrastructure.Models
{
    public class DeveloperModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Headquarters { get; set; }

        // Навігаційна властивість
        public GameModel Game { get; set; }
    }
}
