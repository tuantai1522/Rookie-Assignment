using Rookie.Domain.OrderEntity;

namespace Rookie.WebApi.Controllers.Orders.Request
{
    public sealed record GetListRequest
    {
        public OrderParams? OrderParams { get; set; } = new OrderParams();
    }
}