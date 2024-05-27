namespace Rookie.Mvc.Areas.Customer.Models.Cart
{
    public class CartVm
    {
        public decimal TotalPrice { get; set; }
        public List<CartItemVm> CartItems { get; set; } = [];
    }
}