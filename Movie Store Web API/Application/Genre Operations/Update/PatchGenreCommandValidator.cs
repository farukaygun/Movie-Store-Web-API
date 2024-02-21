using FluentValidation;

namespace Movie_Store_Web_API.Application.Genre_Operations.Update
{
	public class PatchGenreCommandValidator : AbstractValidator<PatchGenreCommand>
	{
		public PatchGenreCommandValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0);
		}
	}
}
