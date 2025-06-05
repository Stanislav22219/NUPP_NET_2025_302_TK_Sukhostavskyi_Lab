using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VideoGames.Infrastructure.Models
{
    public class VideoGamesContext : DbContext
    {
        public VideoGamesContext (DbContextOptions<VideoGamesContext> options)
            : base(options)
        {
        }

        public DbSet<VideoGames.Infrastructure.Models.GameModel> GameModel { get; set; } = default!;
    }
}
