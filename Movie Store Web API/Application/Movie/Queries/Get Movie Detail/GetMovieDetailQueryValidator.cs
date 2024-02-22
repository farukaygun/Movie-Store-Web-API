using FluentValidation;

namespace Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movie_Detail
{
	public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
	{
		public GetMovieDetailQueryValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0);
		}
	}
}
