using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actors;
using Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Directors;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actor_Detail
{
	public class GetActorDetailQuery(IMovieStoreDbContext context,
		IMapper mapper,
		int id)
	{
		public int Id { get; set; } = id;

		public async Task<GetActorModel> Handle()
		{
			var actor = await context.Actors
				.Include(m => m.Movies)
				.Select(a => new GetActorModel
				{
					Id = a.Id,
					Name = a.Name,
					Surname = a.Surname,
					Movies = a.Movies.Select(m => new GetMovieModel
					{
						Id = m.Id,
						Name = m.Name,
						Genres = m.Genres,
						Directors = m.Directors
					}).ToList()
				})
				.SingleOrDefaultAsync(a => a.Id == Id);

			return actor is not null ? mapper.Map<GetActorModel>(actor) : throw new InvalidOperationException("Actor not found!");
		}
	}
}
