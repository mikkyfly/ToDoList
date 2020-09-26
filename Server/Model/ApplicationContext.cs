using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
			
			var converter = new ValueConverter<int[], string>(
				v => string.Join(";", v),
				v => v
					.Split(";", StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse).ToArray());

			var comparer = new ValueComparer<int[]>((c1, c2) => c1.ToHashSet().SetEquals(c2.ToHashSet()), 
					c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
					c => c.ToArray());
			
			modelBuilder.Entity<Order>()
				.Property(o => o.ExecutorIds)
				.HasConversion(converter)
				.Metadata
				.SetValueComparer(comparer);
		}
	}
}