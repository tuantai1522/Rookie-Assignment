using FluentValidation;

namespace Rookie.Application.Categories.Queries.GetByIdQuery
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Id)
                .Must(categoryId => Guid.TryParse(categoryId, out _));
        }
    }
}