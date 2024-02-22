using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies
{
	public class GetMoviesQuery(IMovieStoreDbContext context,
		IMapper mapper)
	{
		public async Task<List<GetMovieModel>> Handle()
		{
			var movies = await context.Movies
				.Include(m => m.Genres)
				.Include(m => m.Actors)
				.Include(m => m.Directors)
				.OrderBy(m => m.Id)
				.ToListAsync();

			return mapper.Map<List<GetMovieModel>>(movies);
		}
	}

	public class GetMovieModel
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public int? Year { get; set; }
		public decimal? Price { get; set; }
		public ICollection<Genre>? Genres;
		public ICollection<Director>? Directors;
		public ICollection<Actor>? Actors;
	}
}
