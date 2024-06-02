
using Rookie.Domain.Common;

namespace Rookie.WebApi.Controllers.Orders.Request
{
    public sealed record CreateRequest
    {
        public Address? ShippingAddress { get; set; }

    }
}