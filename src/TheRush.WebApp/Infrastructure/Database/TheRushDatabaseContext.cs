using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheRush.WebApp.Domain;

namespace TheRush.WebApp.Infrastructure.Database
{
    public class TheRushDatabaseContext : DbContext
    {
        public DbSet<PlayerRushingStats> PlayerRushingStats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=rushing.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerRushingStats>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}