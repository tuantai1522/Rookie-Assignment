namespace Rookie.Mvc.Areas.Customer.Models.Order
{
    public class OrderItemVm
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}