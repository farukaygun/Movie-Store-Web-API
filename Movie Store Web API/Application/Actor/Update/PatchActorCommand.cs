using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Actor_Operations.Update
{
	public class PatchActorCommand(IMovieStoreDbContext context,
		IMapper mapper,
		int id,
		JsonPatchDocument<PatchActorModel> model)
	{
		public int Id { get; set; } = id;
		public JsonPatchDocument<PatchActorModel> Model { get; set; } = model;
		public async Task<PatchActorModel> Handle()
		{
			var actor = await context.Actors
				.SingleOrDefaultAsync(a => a.Id == Id)
				?? throw new InvalidOperationException("Actor not found!");
			
			var actorModel = mapper.Map<PatchActorModel>(actor);

			Model.ApplyTo(actorModel);
			mapper.Map(actorModel, actor);

			await context.SaveChangesAsync();

			return actorModel;
		}
	}

	public class PatchActorModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public ICollection<Movie>? Movies { get; set; }
	}
}
