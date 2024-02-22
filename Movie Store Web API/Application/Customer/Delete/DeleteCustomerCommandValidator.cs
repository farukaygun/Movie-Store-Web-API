using FluentValidation;

namespace Movie_Store_Web_API.Application.Customer_Operations.Delete
{
	public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
	{
		public DeleteCustomerCommandValidator()
		{
			RuleFor(command => command.Id).GreaterThan(0);
		}
	}
}
