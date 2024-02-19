using FluentValidation;

namespace Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actors
{
	public class GetActorsQueryValidator : AbstractValidator<GetActorsQuery>
	{
		public GetActorsQueryValidator()
		{
		}
	}
}
