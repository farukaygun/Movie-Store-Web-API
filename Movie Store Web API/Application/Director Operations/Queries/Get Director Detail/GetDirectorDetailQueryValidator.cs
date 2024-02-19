using FluentValidation;

namespace Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Director_Detail
{
    public class GetDirectorDetailQueryValidator : AbstractValidator<GetDirectorDetailQuery>
    {
        public GetDirectorDetailQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}