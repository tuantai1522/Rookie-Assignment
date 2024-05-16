using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Queries.GetListQuery
{
    public class GetListQuery : IRequest<Result<PagedList<Product>>>
    {
        public ProductParams ProductParams { get; set; }
    }
}