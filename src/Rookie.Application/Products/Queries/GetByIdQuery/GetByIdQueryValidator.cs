using FluentValidation;

namespace Rookie.Application.Products.Queries.GetByIdQuery
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .NotNull();


            RuleFor(x => x.ProductId)
                .Must(ProductId => Guid.TryParse(ProductId, out _));
        }
    }
}