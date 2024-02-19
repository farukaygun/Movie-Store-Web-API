using FluentValidation;

namespace Movie_Store_Web_API.Application.Genre_Operations.Create
{
	public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
	{
		public CreateGenreCommandValidator()
		{
			RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(2);
		}
	}
}
