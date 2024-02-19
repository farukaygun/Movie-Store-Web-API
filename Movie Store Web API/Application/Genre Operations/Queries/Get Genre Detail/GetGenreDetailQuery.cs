using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genres;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genre_Detail
{
	public class GetGenreDetailQuery(IMovieStoreDbContext context,
		IMapper mapper,
		GetGenreModel model)
	{
		public GetGenreModel Model { get; set; } = model;

		public async Task<GetGenreModel> Handle()
		{
			var genre = await context.Genres
				.SingleOrDefaultAsync(x => x.Id == Model.Id || x.Name == Model.Name);

			return genre is not null ? mapper.Map<GetGenreModel>(genre) : throw new InvalidOperationException("Genre not found!");
		}
	}
}
