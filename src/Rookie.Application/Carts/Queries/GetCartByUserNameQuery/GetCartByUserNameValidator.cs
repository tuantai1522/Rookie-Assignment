using FluentValidation;

namespace Rookie.Application.Carts.Queries.GetCartByUserNameQuery
{
    public class GetCartByUserNameValidator : AbstractValidator<GetCartByUserNameQuery>
    {
        public GetCartByUserNameValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();
        }
    }
}