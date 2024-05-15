using MediatR;
using Rookie.Domain.Common;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<Result<int>>
    {
        public string ProductId { get; set; }
    }
}