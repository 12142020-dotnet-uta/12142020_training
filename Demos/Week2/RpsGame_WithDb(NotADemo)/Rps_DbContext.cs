using Microsoft.EntityFrameworkCore;
using System;


namespace RpsGame_NoDb
{
    public class Rps_DbContext : DbContext
    {
        public DbSet<Player> players { get; set; }
        public DbSet<Round> reounds { get; set; }
        public DbSet<Match> matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS02;Database=RpsGame12142020;Trusted_Connection=True;");
        }


    }
}