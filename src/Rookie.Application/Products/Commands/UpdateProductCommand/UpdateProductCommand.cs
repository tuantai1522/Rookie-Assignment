using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest<Result<ProductVm>>
    {
        public ProductId Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public decimal Price { get; set; }
        public CategoryId CategoryId { get; set; }
    }
}