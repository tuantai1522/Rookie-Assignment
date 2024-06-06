using Rookie.Domain.Common;

namespace Rookie.Application.Addresses.Commands.CreateAddressCommand
{
    public class CreateAddressCommand
    {
        public string UserName { get; set; }
        public Address Address { get; set; }
    }
}