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

            RuleFor(x => x.OrderId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.OrderId)
                .Must(OrderId => Guid.TryParse(OrderId, out _));
        }
    }
}