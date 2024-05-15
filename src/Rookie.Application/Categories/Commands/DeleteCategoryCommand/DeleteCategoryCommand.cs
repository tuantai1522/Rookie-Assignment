using MediatR;
using Rookie.Domain.Common;

namespace Rookie.Application.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommand : IRequest<Result<int>>
    {
        public string CategoryId { get; set; }

    }
}