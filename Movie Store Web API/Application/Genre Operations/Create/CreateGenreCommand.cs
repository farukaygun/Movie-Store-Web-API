using AutoMapper;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Genre_Operations.Create
{
	public class CreateGenreCommand(IMovieStoreDbContext context,
		IMapper mapper,
		CreateGenreModel model)
	{
		public CreateGenreModel Model { get; set; } = model;

		public async Task<CreateGenreModel> Handle()
		{
			var genre = context.Genres.SingleOrDefault(x => x.Name == Model.Name);

			if (genre is not null)
				throw new InvalidOperationException("Genre already exist!");

			genre = mapper.Map<Genre>(Model);

			await context.Genres.AddAsync(genre);
			await context.SaveChangesAsync();
			
			return mapper.Map<CreateGenreModel>(genre);
		}
	}

	public class CreateGenreModel
	{
		public int? Id { get; set; }
		public required string Name { get; set; }
	}
}
