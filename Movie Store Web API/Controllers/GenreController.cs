using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_API.Application.Genre_Operations.Create;
using Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genre_Detail;
using Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genres;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Controllers
{
	[Route("api/v1/[controller]s")]
	[ApiController]
	public class GenreController(IMovieStoreDbContext context,
		IMapper mapper) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateGenreModel genre)
		{
			CreateGenreCommand command = new CreateGenreCommand(context, mapper, genre);
			CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

			validator.ValidateAndThrow(command);
			var model = await command.Handle();

			return Ok(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetGenres()
		{
			var command = new GetGenresQuery(context, mapper);
			var validator = new GetGenresQueryValidator();

			validator.ValidateAndThrow(command);
			var genres = await command.Handle();

			return Ok(genres);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var command = new GetGenreDetailQuery(context, mapper, id);
			var validator = new GetGenreDetailQueryValidator();

			validator.ValidateAndThrow(command);
			var genre = await command.Handle();

			return Ok(genre);
		}
	}
}
