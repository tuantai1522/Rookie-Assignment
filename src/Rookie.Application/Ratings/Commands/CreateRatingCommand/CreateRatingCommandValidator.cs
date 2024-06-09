using FluentValidation;

namespace Rookie.Application.Ratings.Commands.CreateRatingCommand
{
    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProductId)
                .Must(ProductId => Guid.TryParse(ProductId, out _));

            RuleFor(x => x.Rating)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);

            RuleFor(x => x.Comment)
                .NotEmpty()
                .NotNull();
        }
    }
}