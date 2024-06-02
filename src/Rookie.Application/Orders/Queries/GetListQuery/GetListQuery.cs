using MediatR;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Orders.Queries.GetListQuery
{
    public class GetListQuery : IRequest<Result<PagedList<OrderVm>>>
    {
        public OrderParams OrderParams { get; set; }
    }
}