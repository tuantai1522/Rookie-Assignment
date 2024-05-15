using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Products.Queries.GetByIdQuery
{
    public class GetByIdQuery : IRequest<Result<ProductVm>>
    {
        public string ProductId { get; set; }
    }
}