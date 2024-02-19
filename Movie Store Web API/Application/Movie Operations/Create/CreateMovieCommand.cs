using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Actor_Operations.Create;
using Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actor_Detail;
using Movie_Store_Web_API.Application.Director_Operations.Create;
using Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Director_Detail;
using Movie_Store_Web_API.Application.Genre_Operations.Create;
using Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genre_Detail;
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

		private async Task<List<CreateGenreModel>> CheckGenresIfExist()
		{
			var getGenreValidator = new GetGenreDetailQueryValidator();
			var createGenreValidator = new CreateGenreCommandValidator();
			var genres = new List<CreateGenreModel>();

			foreach (var genre in Model.Genres)
			{
				CreateGenreModel addedGenre;
				var genreInDb = await context.Genres
					.SingleOrDefaultAsync(x => x.Id == Model.Id || x.Name == Model.Name);

				// creating new genre if it does not exist
				if (genreInDb is null)
				{
					var newGenre = new CreateGenreModel { Name = genre.Name };
					var createGenreCommand = new CreateGenreCommand(context, mapper, newGenre);
					
					createGenreValidator.ValidateAndThrow(createGenreCommand);
					newGenre = await createGenreCommand.Handle();

					addedGenre = newGenre;
				}
				else addedGenre = mapper.Map<CreateGenreModel>(genreInDb);

				genres.Add(addedGenre);
			}
			return genres;
		}

		private async Task<List<CreateActorModel>> CheckActorsIfExist()
		{
			var getActorValidator = new GetActorDetailQueryValidator();
			var createActorValidator = new CreateActorCommandValidator();
			var actors = new List<CreateActorModel>();

			foreach (var actor in Model.Actors)
			{
				CreateActorModel addedActor;

				var getActorCommand = new GetActorDetailQuery(context, mapper, actor.Id); 

				getActorValidator.ValidateAndThrow(getActorCommand);
				var actorInDb = await getActorCommand.Handle();

				// creating new actor if it does not exist
				if (actorInDb is null)
				{
					var newActor = new CreateActorModel { Name = actor.Name, Surname = actor.Surname };
					var createActorCommand = new CreateActorCommand(context, mapper, newActor);
					
					createActorValidator.ValidateAndThrow(createActorCommand);
					newActor = await createActorCommand.Handle();

					addedActor = newActor;
				}
				else addedActor = mapper.Map<CreateActorModel>(actorInDb);

				actors.Add(addedActor);
			}
			return actors;
		}

		private async Task<List<CreateDirectorModel>> CheckDirectorsIfExist()
		{
			var getDirectorValidator = new GetDirectorDetailQueryValidator();
			var createDirectorValidator = new CreateDirectorCommandValidator();
			var directors = new List<CreateDirectorModel>();

			foreach (var director in Model.Directors)
			{
				CreateDirectorModel addedDirector;

				var getDirectorCommand = new GetDirectorDetailQuery(context, mapper, director.Id); 

				getDirectorValidator.ValidateAndThrow(getDirectorCommand);
				var directorInDb = await getDirectorCommand.Handle();

				// creating new director if it does not exist
				if (directorInDb is null)
				{
					var newDirector = new CreateDirectorModel { Name = director.Name, Surname = director.Surname };
					var createDirectorCommand = new CreateDirectorCommand(context, mapper, newDirector);
					
					createDirectorValidator.ValidateAndThrow(createDirectorCommand);
					newDirector = await createDirectorCommand.Handle();

					addedDirector = newDirector;
				}
				else addedDirector = mapper.Map<CreateDirectorModel>(directorInDb);

				directors.Add(addedDirector);
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
