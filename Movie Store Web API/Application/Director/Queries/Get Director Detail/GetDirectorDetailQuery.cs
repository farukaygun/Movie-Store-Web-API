using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Directors;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Director_Detail
{
	public class GetDirectorDetailQuery(IMovieStoreDbContext context,
		IMapper mapper,
		int id)
	{
		public int Id { get; set; } = id;

		public async Task<GetDirectorModel> Handle()
		{
			var director = await context.Directors							
				.Include(m => m.Movies)
				.Select(d => new GetDirectorModel
				{
					Id = d.Id,
					Name = d.Name,
					Surname = d.Surname,
					Movies = d.Movies.Select(m => new GetMovieModel
					{
						Id = m.Id,
						Name = m.Name,
						Genres = m.Genres,
						Actors = m.Actors
					}).ToList()
				})
				.SingleOrDefaultAsync(d => d.Id == Id);

			return director is not null ? mapper.Map<GetDirectorModel>(director) : throw new InvalidOperationException("Director not found!");
		}
	}   
}