using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actors;
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
				.SingleOrDefaultAsync(x => x.Id == Id);

			return actor is not null ? mapper.Map<GetActorModel>(actor) : throw new InvalidOperationException("Actor not found!");
		}
	}
}
