namespace Rookie.Domain.CartEntity
{
    public class Cart
    {
        public decimal TotalPrice { get; set; }
        public List<CartItem> CartItems { get; set; } = [];
    }
}