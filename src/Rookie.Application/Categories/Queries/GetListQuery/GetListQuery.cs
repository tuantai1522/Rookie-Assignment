using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;

namespace Rookie.Application.Categories.Queries.GetListQuery
{
    public class GetListQuery : IRequest<Result<IEnumerable<CategoryVm>>>
    {
    }
}