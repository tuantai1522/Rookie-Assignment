using MediatR;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Orders.Queries.GetListByIdQuery
{
    public class GetListByIdQuery : IRequest<Result<PagedList<OrderVm>>>
    {
        public string UserName { get; set; }
        public OrderParams OrderParams { get; set; }
    }
}