using FluentValidation;

namespace Rookie.Application.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.CategoryId)
                .Must(categoryId => Guid.TryParse(categoryId, out _));
        }
    }
}