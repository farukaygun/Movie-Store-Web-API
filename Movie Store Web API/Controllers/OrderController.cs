using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie_Store_Web_API.Application.Order.Create;
using Movie_Store_Web_API.Application.Order.Queries.Get_Orders;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Controllers
{
	[Authorize]
	[Route("api/v1/[controller]s")]
	[ApiController]
	public class OrderController(IMovieStoreDbContext context,
		IMapper mapper) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateOrderModel order)
		{
			var command = new CreateOrderCommand(context, mapper, order);
			var validator = new CreateOrderCommandValidator();

			validator.ValidateAndThrow(command);
			var model = await command.Handle();

			return Ok(model);
		}

		[HttpGet("{customerId}")]
		public async Task<IActionResult> GetOrders(int customerId)
		{
			var command = new GetOrdersQuery(context, mapper, customerId);
			var validator = new GetOrdersQueryValidator();

			validator.ValidateAndThrow(command);
			var orders = await command.Handle();

			return Ok(orders);
		}
	}
}
