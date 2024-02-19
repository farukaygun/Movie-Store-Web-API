using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genres
{
	public class GetGenresQuery(IMovieStoreDbContext context,
		IMapper mapper)
	{
		public async Task<List<GetGenreModel>> Handle()
		{
			var genres = await context.Genres
				.Include(x => x.Movies)
				.ToListAsync();

			return mapper.Map<List<GetGenreModel>>(genres);
		}
	}

	public class GetGenreModel
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public ICollection<GetMovieModel>? Movies { get; set; }
	}
}
