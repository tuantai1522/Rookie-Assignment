using FluentValidation;

namespace Rookie.Application.Users.Queries.GetAddressByUserNameQuery
{
    public class GetAddressByUserNameValidator : AbstractValidator<GetAddressByUserNameQuery>
    {
        public GetAddressByUserNameValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();
        }
    }
}