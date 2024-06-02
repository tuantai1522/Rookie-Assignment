using System.Globalization;
using FluentValidation;

namespace Rookie.Application.Orders.Queries.GetListQuery
{
    public class GetListQueryValidator : AbstractValidator<GetListQuery>
    {
        public GetListQueryValidator()
        {
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