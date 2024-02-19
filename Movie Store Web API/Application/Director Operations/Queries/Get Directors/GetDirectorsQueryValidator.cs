using FluentValidation;

namespace Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Directors
{
	public class GetDirectorsQueryValidator : AbstractValidator<GetDirectorsQuery>
	{
		public GetDirectorsQueryValidator()
		{
		}
	}
}