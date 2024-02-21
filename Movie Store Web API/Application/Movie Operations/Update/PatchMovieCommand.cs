using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Actor_Operations.Update;
using Movie_Store_Web_API.Application.Director_Operations.Update;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Movie_Operations.Update
{
	public class PatchMovieCommand(IMovieStoreDbContext context,
		IMapper mapper,
		int id,
		JsonPatchDocument<PatchMovieModel> model)
	{
		public int Id { get; set; } = id;
		public JsonPatchDocument<PatchMovieModel> Model { get; set; } = model;
		public async Task<PatchMovieModel> Handle()
		{
			var movie = await context.Movies
				.SingleOrDefaultAsync(m => m.Id == Id)
				?? throw new InvalidOperationException("Movie not found!");
			
			var movieModel = mapper.Map<PatchMovieModel>(movie);

			Model.ApplyTo(movieModel);
			mapper.Map(movieModel, movie);

			await context.SaveChangesAsync();

			return movieModel;
		}
	}

	public class PatchMovieModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int? Year { get; set; }
		public ICollection<PatchGenreModel>? Genre { get; set; }
		public ICollection<PatchDirectorModel>? Director { get; set; }
		public ICollection<PatchActorModel>? Actor { get; set; }
	}
}
