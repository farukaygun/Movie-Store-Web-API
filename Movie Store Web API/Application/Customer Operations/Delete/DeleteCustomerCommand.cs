using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Customer_Operations.Delete
{
	public class DeleteCustomerCommand(IMovieStoreDbContext context, int id)
	{
		public int Id { get; set; } = id;
		public async Task Handle()
		{
			var customer = await context.Customers
				.SingleOrDefaultAsync(c => c.Id == Id) 
				?? throw new InvalidOperationException("Customer not found!");

			context.Customers.Remove(customer);
			await context.SaveChangesAsync();
		}
	}
}