using FluentValidation;

namespace Rookie.Application.Products.Queries.GetListQuery
{
    public class GetListQueryValidator : AbstractValidator<GetListQuery>
    {
        public GetListQueryValidator()
        {
            RuleFor(x => x.ProductParams!.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.ProductParams!.PageSize)
                .GreaterThan(0);
        }
    }

}