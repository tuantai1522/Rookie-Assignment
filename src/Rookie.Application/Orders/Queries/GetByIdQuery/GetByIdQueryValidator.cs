using FluentValidation;

namespace Rookie.Application.Orders.Queries.GetByIdQuery
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.OrderParams!.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.OrderParams!.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.OrderParams!.MinTotal)
                .GreaterThan(0);

            RuleFor(x => x.OrderParams!.MaxTotal)
                .GreaterThan(0);

            RuleFor(x => x.OrderParams!.OrderBy)
                .NotEmpty()
                .NotNull();
        }
    }
}