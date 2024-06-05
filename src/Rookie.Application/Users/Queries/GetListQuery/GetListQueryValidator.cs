using FluentValidation;

namespace Rookie.Application.Users.Queries.GetListQuery
{
    public class GetListQueryValidator : AbstractValidator<GetListQuery>
    {
        public GetListQueryValidator()
        {
            RuleFor(x => x.ApplicationUserParams!.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.ApplicationUserParams!.PageSize)
                .GreaterThan(0);
        }
    }
}