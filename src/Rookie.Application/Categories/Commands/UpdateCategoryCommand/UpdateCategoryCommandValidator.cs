using FluentValidation;

namespace Rookie.Application.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Id)
                .Must(categoryId => Guid.TryParse(categoryId, out _));
        }
    }
}