using FluentValidation;

namespace Movie_Store_Web_API.Application.Actor_Operations.Create
{
	public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
	{
		public CreateActorCommandValidator()
		{
			RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(2);
			RuleFor(x => x.Model.Surname).NotEmpty().MinimumLength(2);
		}
	}
}
