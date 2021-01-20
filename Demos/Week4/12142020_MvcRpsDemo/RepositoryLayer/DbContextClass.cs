using System;
using Microsoft.EntityFrameworkCore;
using ModelLayer;

namespace RepositoryLayer
{
	public class DbContextClass : DbContext
	{
		public DbSet<Player> players { get; set; }
		public DbSet<Round> rounds { get; set; }
		public DbSet<Match> matches { get; set; }

		public DbContextClass() { }
		public DbContextClass(DbContextOptions options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RpsGame12142020;Trusted_Connection=True;");
			}
		}
	}
}
