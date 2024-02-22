using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Director_Operations.Update
{
	public class PatchDirectorCommand(IMovieStoreDbContext context,
		IMapper mapper,
		int id,
		JsonPatchDocument<PatchDirectorModel> model)
	{
		public int Id { get; set; } = id;
		public JsonPatchDocument<PatchDirectorModel> Model { get; set; } = model;
		public async Task<PatchDirectorModel> Handle()
		{
			var director = await context.Directors
				.SingleOrDefaultAsync(d => d.Id == Id)
				?? throw new InvalidOperationException("Director not found!");
			
			var directorModel = mapper.Map<PatchDirectorModel>(director);

			Model.ApplyTo(directorModel);
			mapper.Map(directorModel, director);

			await context.SaveChangesAsync();

			return directorModel;
		}
	}

	public class PatchDirectorModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public ICollection<Movie>? Movies { get; set; }
	}
}
