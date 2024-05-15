using FluentValidation;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(c => c.ProductId)
                .NotEmpty().WithMessage("Please provide id of product to delete")
                .NotNull();
        }
    }
}