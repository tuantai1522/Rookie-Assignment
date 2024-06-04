namespace Rookie.Mvc.Areas.Customer.Models.Order
{
    public class ShippingAddressVm
    {
        public Address ShippingAddress { get; set; }
        public ShippingAddressVm(Address ShippingAddress)
        {
            this.ShippingAddress = ShippingAddress;
        }
    }
}