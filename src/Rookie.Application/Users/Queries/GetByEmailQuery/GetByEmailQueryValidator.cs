using FluentValidation;

namespace Rookie.Application.Users.Queries.GetByEmailQuery
{
    public class GetByEmailQueryValidator : AbstractValidator<GetByEmailQuery>
    {
        public GetByEmailQueryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();
        }
    }
}