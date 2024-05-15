using MediatR;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;
namespace Rookie.Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommand : IRequest<Result<ProductId>>
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
    }
}