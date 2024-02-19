using FluentValidation;

namespace Movie_Store_Web_API.Application.Movie_Operations.Create
{
	public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
	{
		public CreateMovieCommandValidator()
		{
			RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
			//RuleFor(command => command.Model.Directors).NotEmpty();
			//RuleFor(command => command.Model.Genres).NotEmpty();
			RuleFor(command => command.Model.Price).GreaterThan(0);
			RuleFor(command => command.Model.Year).LessThanOrEqualTo(DateTime.Now.Year);
		}
	}
}
