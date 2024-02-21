using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_API.Application.Director_Operations.Create;
using Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Director_Detail;
using Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Directors;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Controllers
{
	[Route("api/v1/[controller]s")]
	[ApiController]
	public class DirectorController(IMovieStoreDbContext context,
		IMapper mapper) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateDirectorModel director)
		{
			CreateDirectorCommand command = new CreateDirectorCommand(context, mapper, director);
			CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();

			validator.ValidateAndThrow(command);
			var model = await command.Handle();

			return Ok(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetDirectors()
		{
			var command = new GetDirectorsQuery(context, mapper);
			var validator = new GetDirectorsQueryValidator();

			validator.ValidateAndThrow(command);
			var directors = await command.Handle();

			return Ok(directors);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var command = new GetDirectorDetailQuery(context, mapper, id);
			var validator = new GetDirectorDetailQueryValidator();

			validator.ValidateAndThrow(command);
			var director = await command.Handle();

			return Ok(director);
		}
	}
}
