using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Model;

namespace Server.Model
{
	public class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; }

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

			modelBuilder.Entity<User>()
				.HasAlternateKey("Login");

			modelBuilder.Entity<Order>().HasOne(o => o.Customer);

			modelBuilder.Entity<Order>().HasMany(o => o.Executors);
		}
	}
}