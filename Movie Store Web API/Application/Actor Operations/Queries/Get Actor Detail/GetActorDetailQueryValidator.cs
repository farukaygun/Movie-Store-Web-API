using FluentValidation;

namespace Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actor_Detail
{
	public class GetActorDetailQueryValidator : AbstractValidator<GetActorDetailQuery>
	{
		public GetActorDetailQueryValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0);
		}
	}
}
