using Microsoft.EntityFrameworkCore;
using System;

namespace RpsGame_NoDb
{
    public class RpsDbContext : DbContext
    {
        public DbSet<Player> players { get; set; }
        public DbSet<Round> rounds { get; set; }
        public DbSet<Match> matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS02;Database=RpsGame12142020;Trusted_Connection=True;");
        }


    }
}