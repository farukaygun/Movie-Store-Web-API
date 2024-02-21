using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_API.Application.Actor_Operations.Create;
using Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actor_Detail;
using Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actors;
using Movie_Store_Web_API.Application.Actor_Operations.Update;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Controllers
{
	[Route("api/v1/[controller]s")]
	[ApiController]
	public class ActorController(IMovieStoreDbContext context,
		IMapper mapper) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateActorModel actor)
		{
			var command = new CreateActorCommand(context, mapper, actor);
			var validator = new CreateActorCommandValidator();

			validator.ValidateAndThrow(command);
			var model = await command.Handle();

			return Ok(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetActors()
		{
			var command = new GetActorsQuery(context, mapper);
			var validator = new GetActorsQueryValidator();

			validator.ValidateAndThrow(command);
			var actors = await command.Handle();

			return Ok(actors);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var command = new GetActorDetailQuery(context, mapper, id);
			var validator = new GetActorDetailQueryValidator();

			validator.ValidateAndThrow(command);
			var actor = await command.Handle();

			return Ok(actor);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<PatchActorModel> patch)
		{
			var command = new PatchActorCommand(context, mapper, id, patch);
			var validator = new PatchActorCommandValidator();

			validator.ValidateAndThrow(command);
			var actor = await command.Handle();

			return Ok(actor);
		}
	}
}
