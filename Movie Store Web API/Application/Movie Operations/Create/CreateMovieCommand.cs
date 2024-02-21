using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Movie_Operations.Create
{
	public class CreateMovieCommand(IMovieStoreDbContext context,
		IMapper mapper,
		CreateMovieModel model)
	{
		public CreateMovieModel Model { get; set; } = model;

		public async Task<CreateMovieModel> Handle()
		{
			var movie = await context.Movies.FirstOrDefaultAsync(x => x.Name == Model.Name);

			if (movie is not null)
				throw new InvalidOperationException("Movie already exist!");

			var genres = await CheckGenresIfExist();
			var actors = await CheckActorsIfExist();
			var directors = await CheckDirectorsIfExist();

			movie = mapper.Map<Movie>(Model);
			movie.Genres = mapper.Map<List<Genre>>(genres);
			movie.Actors = mapper.Map<List<Actor>>(actors);
			movie.Directors = mapper.Map<List<Director>>(directors);

			context.Entry(movie).State = EntityState.Added;
			await context.AddAsync(movie);
			await context.SaveChangesAsync();

			return mapper.Map<CreateMovieModel>(movie);
		}

		private async Task<List<Genre>> CheckGenresIfExist()
		{
			var genres = new List<Genre>();
			foreach (var genre in Model.Genres)
			{
				var existedGenre = await context.Genres
					.SingleOrDefaultAsync(x => x.Id == genre.Id || x.Name == genre.Name) 
					?? throw new NullReferenceException($"Genre {genre.Name} not found!");

				genres.Add(existedGenre);
			}
			return genres;
		}

		private async Task<List<Actor>> CheckActorsIfExist()
		{
			var actors = new List<Actor>();

			foreach (var actor in Model.Actors)
			{
				var existedActor = await context.Actors
					.SingleOrDefaultAsync(x => x.Id == actor.Id || (x.Name == actor.Name && x.Surname == actor.Surname))
					?? throw new NullReferenceException($"Actor {actor.Name} not found!");

				actors.Add(existedActor);
			}
			return actors;
		}

		private async Task<List<Director>> CheckDirectorsIfExist()
		{
			var directors = new List<Director>();

			foreach (var director in Model.Directors)
			{
				var existedDirector = await context.Directors
					.SingleOrDefaultAsync(x => x.Id == director.Id || (x.Name == director.Name && x.Surname == director.Surname))
					?? throw new NullReferenceException($"Director {director.Name} not found!");

				directors.Add(existedDirector);
			}
			return directors;
		}
	}

	public class CreateMovieModel
	{
		public int? Id { get; set; }
		public required string Name { get; set; }
		public required int Year { get; set; }
		public required decimal Price { get; set; }
		public required ICollection<Genre> Genres { get; set; } = [];
		public required ICollection<Director> Directors { get; set; } = [];
		public required ICollection<Actor> Actors { get; set; } = [];
	}
}
