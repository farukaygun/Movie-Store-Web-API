using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Genre_Operations.Update
{
	public class PatchGenreCommand(IMovieStoreDbContext context,
		IMapper mapper,
		int id,
		JsonPatchDocument<PatchGenreModel> model)
	{
		public int Id { get; set; } = id;
		public JsonPatchDocument<PatchGenreModel> Model { get; set; } = model;
		public async Task<PatchGenreModel> Handle()
		{
			var genre = await context.Genres
				.SingleOrDefaultAsync(g => g.Id == Id)
				?? throw new InvalidOperationException("Genre not found!");
			
			var genreModel = mapper.Map<PatchGenreModel>(genre);

			Model.ApplyTo(genreModel);
			mapper.Map(genreModel, genre);

			await context.SaveChangesAsync();

			return genreModel;
		}
	}

	public class PatchGenreModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}
