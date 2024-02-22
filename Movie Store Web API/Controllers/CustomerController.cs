using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_API.Application.Customer_Operations.Create;
using Movie_Store_Web_API.Application.Customer_Operations.Create_Token;
using Movie_Store_Web_API.Application.Customer_Operations.Delete;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Controllers
{
	[Route("api/v1/[controller]s")]
	[ApiController]
	public class CustomerController(IMovieStoreDbContext context,
		IMapper mapper,
		IConfiguration configuration) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateCustomerModel customer)
		{
			var command = new CreateCustomerCommand(context, mapper, customer);
			var validator = new CreateCustomerCommandValidator();

			validator.ValidateAndThrow(command);
			var model = await command.Handle();
			
			return Ok(model);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new DeleteCustomerCommand(context, id);
			var validator = new DeleteCustomerCommandValidator();

			validator.ValidateAndThrow(command);
			await command.Handle();

			return Ok();
		}


		[HttpPost("connect/token")]
		public async Task<IActionResult> CreateToken([FromBody] CreateTokenModel login)
		{
			var command = new CreateTokenCommand(context, configuration, login);
			var token = await command.Handle();

			return Ok(token);
		}

		[HttpGet("refreshToken")]
		public async Task<IActionResult> RefreshToken([FromQuery] string token)
		{
			var command = new RefreshTokenCommand(context, configuration, token);
			var newToken = await command.Handle();

			return Ok(newToken);
		}
	}
}
