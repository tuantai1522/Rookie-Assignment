
using Rookie.Domain.Common;

namespace Rookie.WebApi.Controllers.Orders.Request
{
    public class CreateRequest
    {
        public Address? ShippingAddress { get; set; }

    }
}