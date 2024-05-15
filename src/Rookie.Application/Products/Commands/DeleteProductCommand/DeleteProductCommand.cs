using MediatR;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<Result<int>>
    {
        public ProductId ProductId { get; set; }
    }
}