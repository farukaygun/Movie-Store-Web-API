using FluentValidation;

namespace Movie_Store_Web_API.Application.Director_Operations.Update
{
	public class PatchDirectorCommandValidator : AbstractValidator<PatchDirectorCommand>
	{
		public PatchDirectorCommandValidator()
		{
			RuleFor(command => command.Id).GreaterThan(0);
			RuleFor(command => command.Model).NotEmpty();
		}
	}
}
