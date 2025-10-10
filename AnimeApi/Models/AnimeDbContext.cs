using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AnimeApi.Models
{
    public class AnimeDbContext : DbContext
    {
        public AnimeDbContext(DbContextOptions<AnimeDbContext> options) : base(options)
        {
        }

        // DbSet properties represent tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Anime> Anime { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }

        // Configure relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure Email is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Ensure Anime Code is unique
            modelBuilder.Entity<Anime>()
                .HasIndex(a => a.Code)
                .IsUnique();

            // Configure One-to-Many: User -> Watchlists
            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Watchlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure One-to-Many: Anime -> Watchlists
            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.Anime)
                .WithMany(a => a.Watchlists)
                .HasForeignKey(w => w.AnimeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
