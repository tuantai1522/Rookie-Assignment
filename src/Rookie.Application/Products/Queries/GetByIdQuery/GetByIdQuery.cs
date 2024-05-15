using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Queries.GetByIdQuery
{
    public class GetByIdQuery : IRequest<ProductVm>
    {
        public ProductId ProductId { get; set; }
    }
}