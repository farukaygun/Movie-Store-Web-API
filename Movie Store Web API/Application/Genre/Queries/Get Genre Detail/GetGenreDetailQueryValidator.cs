using FluentValidation;

namespace Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genre_Detail
{
	public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
	{
		public GetGenreDetailQueryValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0);
		}
	}
}
