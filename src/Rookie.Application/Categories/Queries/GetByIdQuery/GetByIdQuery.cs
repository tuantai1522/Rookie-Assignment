using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;

namespace Rookie.Application.Categories.Queries.GetByIdQuery
{
    public class GetByIdQuery : IRequest<CategoryVm>
    {
        public CategoryId Id { get; set; }
    }

}