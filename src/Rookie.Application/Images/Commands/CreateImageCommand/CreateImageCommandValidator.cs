using FluentValidation;

namespace Rookie.Application.Images.Commands.CreateImageCommand
{
    public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        public CreateImageCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProductId)
                .Must(ProductId => Guid.TryParse(ProductId, out _));

            RuleFor(x => x.FileImage)
                .NotEmpty()
                .NotNull();
        }
    }
}