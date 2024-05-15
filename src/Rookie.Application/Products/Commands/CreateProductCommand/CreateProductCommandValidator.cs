using FluentValidation;

namespace Rookie.Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
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