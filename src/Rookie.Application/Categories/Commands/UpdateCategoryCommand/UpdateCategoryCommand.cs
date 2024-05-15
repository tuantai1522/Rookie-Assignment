using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommand : IRequest<Result<CategoryVm>>
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}