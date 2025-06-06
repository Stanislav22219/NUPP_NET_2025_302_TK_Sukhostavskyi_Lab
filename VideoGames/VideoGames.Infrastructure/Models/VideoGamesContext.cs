﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace VideoGames.Infrastructure.Models
{
    public class VideoGamesContext : IdentityDbContext<ApplicationUser>
    {
        public VideoGamesContext(DbContextOptions<VideoGamesContext> options) : base(options) { }

        public DbSet<GameModel> Games { get; set; }
        public DbSet<DeveloperModel> Developers { get; set; }
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Один-до-багатьох: GameModel та PlayerModel через GamePlayer
            modelBuilder.Entity<PlayerModel>()
                .HasMany(p => p.GamePlayers)
                .WithOne(gp => gp.Player)
                .HasForeignKey(gp => gp.PlayerId);
        }
    }
}
