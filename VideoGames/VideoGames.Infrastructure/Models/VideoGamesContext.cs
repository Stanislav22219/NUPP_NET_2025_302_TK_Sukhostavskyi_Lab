using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGames.Infrastructure.Models;

namespace VideoGames.Infrastructure.Models
{
    public class VideoGamesContext : DbContext
    {
        public VideoGamesContext(DbContextOptions<VideoGamesContext> options) : base(options) { }
        public DbSet<GameModel> Games { get; set; }
        public DbSet<DeveloperModel> Developers { get; set; }
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VideoGamesDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Багато-до-багатьох: Гравці та Ігри через GamePlayer
            modelBuilder.Entity<GamePlayer>()
                .HasKey(gp => new { gp.GameId, gp.PlayerId });

            modelBuilder.Entity<GamePlayer>()
                .HasOne(gp => gp.Game)
                .WithMany(g => g.GamePlayers)
                .HasForeignKey(gp => gp.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GamePlayer>()
                .HasOne(gp => gp.Player)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(gp => gp.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Один-до-одного: GameModel та DeveloperModel
            modelBuilder.Entity<GameModel>()
                .HasOne(g => g.Developer)
                .WithOne(d => d.Game)
                .HasForeignKey<DeveloperModel>(d => d.Id);

            // Один-до-багатьох: GameModel та PlayerModel
            modelBuilder.Entity<GameModel>()
                .HasMany(g => g.Players)
                .WithOne(p => p.GameModel)
                .HasForeignKey(p => p.GameModelId);
        }
    }
}
