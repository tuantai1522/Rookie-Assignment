using FluentValidation;

namespace Rookie.Application.Addresses.Commands.CreateAddressCommand
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Address.ZipCode)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Address.Value)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Address.City)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Address.Country)
                .NotNull()
                .NotEmpty();
        }
    }
}