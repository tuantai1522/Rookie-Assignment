using FluentValidation;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(c => c.ProductId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProductId)
                .Must(ProductId => Guid.TryParse(ProductId, out _));
        }
    }
}