using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actors
{
	public class GetActorsQuery(IMovieStoreDbContext context,
		IMapper mapper)
	{
		public async Task<List<GetActorModel>> Handle()
		{
			var actors = await context.Actors
				.OrderBy(a => a.Id)
				.ToListAsync();

			return mapper.Map<List<GetActorModel>>(actors);
		}
	}

	public class GetActorModel
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public ICollection<GetMovieModel>? Movies { get; set; }
	}
}
