using Rookie.Domain.Common;

namespace Rookie.Application.Addresses.ViewModels
{
    public class ApplicationUserAddressVm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public Address Address { get; set; }
    }
}