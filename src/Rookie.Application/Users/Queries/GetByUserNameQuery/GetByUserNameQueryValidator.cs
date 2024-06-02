using FluentValidation;

namespace Rookie.Application.Users.Queries.GetByUserNameQuery
{
    public class GetByUserNameQueryValidator : AbstractValidator<GetByUserNameQuery>
    {
        public GetByUserNameQueryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();
        }
    }
}