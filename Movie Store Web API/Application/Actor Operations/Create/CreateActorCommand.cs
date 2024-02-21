using AutoMapper;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Actor_Operations.Create
{
	public class CreateActorCommand(IMovieStoreDbContext context,
		IMapper mapper,
		CreateActorModel model)
	{
		public CreateActorModel Model { get; set; } = model;

		public async Task<CreateActorModel> Handle()
		{
			var actor = context.Actors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);

			if (actor is not null)
				throw new InvalidOperationException("Actor already exist!");

			actor = mapper.Map<Actor>(Model);

			await context.Actors.AddAsync(actor);
			await context.SaveChangesAsync();
			
			return mapper.Map<CreateActorModel>(actor);
		}
	}

	public class CreateActorModel
	{
		public int? Id { get; set; }
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public ICollection<Movie> Movies { get; set; } = []; 
	}
}
