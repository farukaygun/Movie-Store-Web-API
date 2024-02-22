using FluentValidation;

namespace Movie_Store_Web_API.Application.Actor_Operations.Update
{
	public class PatchActorCommandValidator : AbstractValidator<PatchActorCommand>
	{
		public PatchActorCommandValidator()
		{
			RuleFor(command => command.Id).GreaterThan(0);
			RuleFor(command => command.Model).NotEmpty();
		}
	}
}
