using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest<Result<ProductVm>>
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
    }
}