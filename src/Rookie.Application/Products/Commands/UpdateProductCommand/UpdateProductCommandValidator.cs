
using FluentValidation;

namespace Rookie.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Please provide id of product to update")
                .NotNull();

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Please provide product name of this product")
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Please provide description of this product")
                .NotNull();

            RuleFor(x => x.Images)
                .NotEmpty().WithMessage("Please provide link image of this product")
                .NotNull();

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Please provide type of this product")
                .NotNull();

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Please provide price of this product")
                .NotNull()
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}