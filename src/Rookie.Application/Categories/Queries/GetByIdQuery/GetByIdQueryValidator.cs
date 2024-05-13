using FluentValidation;

namespace Rookie.Application.Categories.Queries.GetByIdQuery
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Please provide id to find course")
                .NotNull();
        }
    }
}