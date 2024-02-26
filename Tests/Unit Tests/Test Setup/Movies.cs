using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests.Test_Setup
{
	internal static class Movies
	{
		public static void AddMovies(this IMovieStoreDbContext context)
		{
			context.Movies.AddRange(
				new Movie
				{
					Name = "Inception",
					Price = 12,
					Year = 2018,
					Genres = [
						new Genre { Name = "Sci-Fi" }
					],
					Actors = [
						new Actor { Name = "Leonardo", Surname = "DiCaprio" },
					],
					Directors = [
						new Director { Name = "Christopher", Surname = "Nolan" }
					]
				}												
			);
		}
	}
}
