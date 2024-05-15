
using FluentValidation;

namespace Rookie.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Images)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Id)
                .Must(ProductId => Guid.TryParse(ProductId, out _));

            RuleFor(x => x.CategoryId)
                .Must(CategoryId => Guid.TryParse(CategoryId, out _));
        }
    }
}