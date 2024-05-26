using FluentValidation;

namespace Rookie.Application.Carts.Commands.ChangeCartQuantityCommand
{
    public class ChangeCartQuantityCommandValidator : AbstractValidator<ChangeCartQuantityCommand>
    {
        public ChangeCartQuantityCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull();
        }
    }
}