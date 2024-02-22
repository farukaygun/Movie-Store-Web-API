using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actors;
using Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genres;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genre_Detail
{
	public class GetGenreDetailQuery(IMovieStoreDbContext context,
		IMapper mapper,
		int id)
	{
		public int Id { get; set; } = id;

		public async Task<GetGenreModel> Handle()
		{
			var genre = await context.Genres
				.Include(m => m.Movies)
				.Select(a => new GetGenreModel
				{
					Id = a.Id,
					Name = a.Name,
					Movies = a.Movies.Select(m => new GetMovieModel
					{
						Id = m.Id,
						Name = m.Name,
						Actors = m.Actors,
						Directors = m.Directors
					}).ToList()
				})
				.SingleOrDefaultAsync(g => g.Id == Id);

			return genre is not null ? mapper.Map<GetGenreModel>(genre) : throw new InvalidOperationException("Genre not found!");
		}
	}
}
