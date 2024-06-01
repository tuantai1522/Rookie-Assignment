using Rookie.Domain.Common;

namespace Rookie.WebApi.Controllers.Orders.Request
{
    public class CreateOrderRequest
    {
        public Address? ShippingAddress { get; set; }
    }
}