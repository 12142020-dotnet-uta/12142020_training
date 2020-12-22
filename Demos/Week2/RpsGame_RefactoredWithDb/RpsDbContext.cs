using Microsoft.EntityFrameworkCore;

namespace RpsGame_NoDb
{
    public class RpsDbContext : DbContext
    {
        DbSet<Player> players { get; set; }
        DbSet<Round> rounds { get; set; }
        DbSet<Match> matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS02;Database=RpsGame12142020;Trusted_Connection=True;");
        }


    }
}