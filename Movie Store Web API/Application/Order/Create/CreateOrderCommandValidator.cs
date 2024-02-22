using FluentValidation;

namespace Movie_Store_Web_API.Application.Order.Create
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(o => o.Model.Customer).NotEmpty();
			RuleFor(o => o.Model.Movies).NotEmpty();
			RuleFor(o => o.Model.Movies.Count).GreaterThan(0);
			RuleFor(o => o.Model.Price).GreaterThanOrEqualTo(0);
		}
	}
}
