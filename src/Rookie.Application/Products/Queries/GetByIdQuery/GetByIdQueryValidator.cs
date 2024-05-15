using FluentValidation;

namespace Rookie.Application.Products.Queries.GetByIdQuery
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Please provide id to find product")
                .NotNull();
        }
    }
}