namespace Rookie.Mvc.Areas.Customer.Models.Order
{
    public class OrderVm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public Address ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public ICollection<OrderItemVm> OrderItems { get; set; } = [];
    }
}