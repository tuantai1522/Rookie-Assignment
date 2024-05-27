
namespace Rookie.Mvc.Areas.Customer.Models.Cart
{
    public class CartItemVm
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public decimal TotalPrice { get; set; }
    }
}