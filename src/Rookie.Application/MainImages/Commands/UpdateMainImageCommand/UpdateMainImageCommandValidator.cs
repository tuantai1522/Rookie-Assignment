using FluentValidation;

namespace Rookie.Application.MainImages.Commands.UpdateMainImageCommand
{
    public class UpdateMainImageCommandValidator : AbstractValidator<UpdateMainImageCommand>
    {
        public UpdateMainImageCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ProductId)
                .Must(ProductId => Guid.TryParse(ProductId, out _));

            RuleFor(x => x.ImageId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ImageId)
                .Must(ImageId => Guid.TryParse(ImageId, out _));
        }
    }
}