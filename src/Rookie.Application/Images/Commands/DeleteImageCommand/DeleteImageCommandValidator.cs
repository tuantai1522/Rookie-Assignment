using FluentValidation;

namespace Rookie.Application.Images.Commands.DeleteImageCommand
{
    public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageCommandValidator()
        {
            RuleFor(c => c.ImageId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ImageId)
                .Must(ImageId => Guid.TryParse(ImageId, out _));
        }
    }
}