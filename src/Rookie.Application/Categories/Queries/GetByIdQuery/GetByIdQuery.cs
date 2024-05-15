using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Categories.Queries.GetByIdQuery
{
    public class GetByIdQuery : IRequest<Result<CategoryVm>>
    {
        public string Id { get; set; }
    }

}