using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Order.Create
{
	using Order = Entities.Order;
	public class CreateOrderCommand(IMovieStoreDbContext context,
		IMapper mapper,
		CreateOrderModel model)
	{
		public CreateOrderModel Model { get; set; } = model;
		public async Task<CreateOrderModel> Handle()
		{
			var customer = CheckCustomerIfExist();
			var movies = CheckMoviesIfExist();

			var order = mapper.Map<Order>(Model);
			order.Customer = mapper.Map<Customer>(customer);
			order.Movies = mapper.Map<List<Movie>>(movies);

			context.Entry(order).State = EntityState.Added;
			await context.AddAsync(order);
			await context.SaveChangesAsync();

			return mapper.Map<CreateOrderModel>(order);
		}

		private async Task<Customer> CheckCustomerIfExist()
		{
			var customer = await context.Customers
				.SingleOrDefaultAsync(c => c.Id == Model.Customer.Id)
				?? throw new InvalidOperationException("Customer not found!");

			return customer;


		}

		private async Task<List<Movie>> CheckMoviesIfExist()
		{
			var movies = new List<Movie>();

			foreach (var movie in Model.Movies)
			{
				var existedMovie = await context.Movies
					.SingleOrDefaultAsync(x => x.Id == movie.Id || x.Name == movie.Name)
					?? throw new NullReferenceException($"Movie {movie.Name} not found!");

				movies.Add(existedMovie);
			}
			return movies;
		}
	}

	public class CreateOrderModel
	{
		public required Customer Customer { get; set; }
		public required ICollection<Movie> Movies { get; set; }
		public required double Price { get; set; }
		public DateTime? Date { get; set; }	
	}
}
