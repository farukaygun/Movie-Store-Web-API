using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Order.Queries.Get_Orders;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Order.Queries.Get_Order_Details
{
	public class GetOrderDetailQuery(IMovieStoreDbContext context,
		IMapper mapper,
		int id)
	{
		public int Id { get; set; } = id;

		public async Task<GetOrderModel> Handle()
		{
			var order = await context.Orders
				.Include(o => o.Customer)
				.Include(o => o.Movies)
				.SingleOrDefaultAsync(o => o.Id == Id);

			return order is not null ? mapper.Map<GetOrderModel>(order) : throw new InvalidOperationException("Order not found!");
		}
	}
}
