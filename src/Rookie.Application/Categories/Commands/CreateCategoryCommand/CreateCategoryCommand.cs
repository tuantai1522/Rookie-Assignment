using MediatR;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;

namespace Rookie.Application.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommand : IRequest<Result<CategoryId>>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}