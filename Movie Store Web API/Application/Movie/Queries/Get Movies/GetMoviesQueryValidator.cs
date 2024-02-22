using FluentValidation;

namespace Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies
{
	public class GetMoviesQueryValidator : AbstractValidator<GetMoviesQuery>
	{
		public GetMoviesQueryValidator()
		{
		}
	}
}
