using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Customer_Operations.Create
{
	public class CreateCustomerCommand(IMovieStoreDbContext context,
		IMapper mapper,
		CreateCustomerModel model)
	{
		public CreateCustomerModel Model { get; set; } = model;
		public async Task<CreateCustomerModel> Handle()
		{
			var customer = await context.Customers
				.SingleOrDefaultAsync(c => c.Email == Model.Email);
			
			if (customer is not null)
				throw new InvalidOperationException("Customer already exist!");

			customer = mapper.Map<Customer>(Model);

			await context.Customers.AddAsync(customer);
			await context.SaveChangesAsync();

			return mapper.Map<CreateCustomerModel>(customer);
		}
	}

	public class CreateCustomerModel
	{
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
	}

}
