using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Db_Operations
{
	public abstract class IMovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : DbContext(options)
	{
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Director> Directors { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}
