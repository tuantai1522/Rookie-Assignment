using FluentValidation;

namespace Rookie.Application.Orders.Commands.CreateOrderCommand
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ShippingAddress.ZipCode)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ShippingAddress.Value)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ShippingAddress.City)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ShippingAddress.Country)
                .NotNull()
                .NotEmpty();
        }
    }
}