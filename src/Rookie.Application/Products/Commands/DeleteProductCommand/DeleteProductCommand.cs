using MediatR;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<int>
    {
        public ProductId ProductId { get; set; }
    }
}