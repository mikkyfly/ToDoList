using Microsoft.EntityFrameworkCore;

using Model;

namespace Server.Model
{
	public class ApplicationContext : DbContext
	{
		public DbSet<PublicUser> PublicUsers { get; set; }

		public DbSet<Order> Orders { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<PublicUser>()
				.HasAlternateKey("Login");

			modelBuilder.Entity<Order>().HasOne(o => o.Customer);

			modelBuilder.Entity<Order>().HasMany(o => o.Executors);
		}
	}
}