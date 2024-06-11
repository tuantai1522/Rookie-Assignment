using FluentValidation;

namespace Rookie.Application.Ratings.Queries
{
    public class GetListQueryValidator : AbstractValidator<GetListQuery>
    {
        public GetListQueryValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.RatingParams!.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.RatingParams!.PageSize)
                .GreaterThan(0);
        }
    }
}