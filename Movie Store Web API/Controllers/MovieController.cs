using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_API.Application.Movie_Operations.Create;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movie_Detail;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Controllers
{
	[Route("api/v1/[controller]s")]
	[ApiController]
	public class MovieController(IMovieStoreDbContext context,
		IMapper mapper) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateMovieModel movie)
		{
			CreateMovieCommand command = new CreateMovieCommand(context, mapper, movie);
			CreateMovieCommandValidator validator = new CreateMovieCommandValidator();

			validator.ValidateAndThrow(command);
			var model = await command.Handle();

			return Ok(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetMovies()
		{
			var command = new GetMoviesQuery(context, mapper);
			var validator = new GetMoviesQueryValidator();

			validator.ValidateAndThrow(command);
			var movies = await command.Handle();

			return Ok(movies);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var command = new GetMovieDetailQuery(context, mapper, id);
			var validator = new GetMovieDetailQueryValidator();

			validator.ValidateAndThrow(command);
			var movie = await command.Handle();

			return Ok(movie);
		}
	}
}
