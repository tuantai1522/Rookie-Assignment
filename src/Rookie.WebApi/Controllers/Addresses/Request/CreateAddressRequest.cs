using Rookie.Domain.Common;

namespace Rookie.WebApi.Controllers.Addresses.Request
{
    public sealed record CreateAddressRequest
    {
        public Address? Address { get; set; }
    }
}