
using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Mvc.Areas.Customer.Models.Order
{
    public class AddressViewModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class UserAddressViewModel
    {
        public List<AddressViewModel> Addresses { get; set; }
        public int SelectedAddressIndex { get; set; } // Index of the selected address
        public AddressViewModel SelectedAddress { get; set; } // The currently selected address
    }
}