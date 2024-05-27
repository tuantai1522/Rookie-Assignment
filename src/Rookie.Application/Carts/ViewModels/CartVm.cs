namespace Rookie.Application.Carts.ViewModels
{
    public class CartVm
    {
        public decimal TotalPrice { get; set; }
        public List<CartItemVm> CartItems { get; set; } = [];
    }
}