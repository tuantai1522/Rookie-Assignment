using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Queries.GetListQuery
{
    public class GetListQuery : IRequest<IEnumerable<ProductVm>>
    {
        public ProductParams? ProductParams { get; set; }

    }
}