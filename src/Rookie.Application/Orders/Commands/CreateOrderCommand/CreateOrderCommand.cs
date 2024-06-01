using MediatR;
using Rookie.Domain.Common;
using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Orders.Commands.CreateOrderCommand
{
    public class CreateOrderCommand : IRequest<Result<OrderId>>
    {
        public string UserName { get; set; }
        public Address ShippingAddress { get; set; }
    }
}