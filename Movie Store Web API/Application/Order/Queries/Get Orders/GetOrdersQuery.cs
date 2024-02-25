using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Order.Queries.Get_Orders
{
	public class GetOrdersQuery(IMovieStoreDbContext context,
		IMapper mapper,
		int customerId)
	{
		public async Task<List<GetOrderModel>> Handle()
		{
			var orders = await context.Orders
				.Where(o => o.Customer.Id == customerId)
				.Include(o => o.Customer)
				.Include(o => o.Movies)
				.OrderBy(o => o.Id)
				.ToListAsync();

			return mapper.Map<List<GetOrderModel>>(orders);
		}
	}

	public class GetOrderModel()
	{
		public int? Id { get; set; }
		public Customer? Customer { get; set; }
		public List<Movie>? Movies { get; set; }
		public double? Price { get; set; }
		public DateTime? Date { get; set; }

	}
}
