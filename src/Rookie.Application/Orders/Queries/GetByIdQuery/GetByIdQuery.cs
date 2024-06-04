using MediatR;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Orders.Queries.GetByIdQuery
{
    public class GetByIdQuery : IRequest<Result<PagedList<OrderVm>>>
    {
        public string UserName { get; set; }
        public OrderParams OrderParams { get; set; }
    }
}