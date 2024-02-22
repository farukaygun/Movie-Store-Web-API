using FluentValidation;

namespace Movie_Store_Web_API.Application.Order.Queries.Get_Orders
{
	public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
	{
		public GetOrdersQueryValidator()
		{
			// RuleFor(query => query.).NotEmpty();
		}
	}
}
