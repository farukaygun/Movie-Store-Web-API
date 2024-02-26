using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Unit_Tests.Test_Setup
{
	internal static class Genres
	{
		public static void AddGenres(this IMovieStoreDbContext context)
		{
			context.Genres.AddRange(
				new Genre { Name = "Sci-Fi" },
				new Genre { Name = "Adventure" }
			);
		}
	}
}
