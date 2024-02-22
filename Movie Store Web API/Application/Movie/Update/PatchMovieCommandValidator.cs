using FluentValidation;

namespace Movie_Store_Web_API.Application.Movie_Operations.Update
{
	public class PatchMovieCommandValidator : AbstractValidator<PatchMovieCommand>
	{
		public PatchMovieCommandValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0);
		}
	}
}
