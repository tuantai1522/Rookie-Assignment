using MediatR;
using Rookie.Application.Categories.ViewModels;

namespace Rookie.Application.Categories.Queries.GetListQuery
{
    public class GetListQuery : IRequest<IEnumerable<CategoryVm>>
    {

    }
}