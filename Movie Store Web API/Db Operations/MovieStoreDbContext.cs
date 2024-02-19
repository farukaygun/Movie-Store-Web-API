using Microsoft.EntityFrameworkCore;

namespace Movie_Store_Web_API.Db_Operations
{
	public class MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : IMovieStoreDbContext(options)
	{
	}
}
