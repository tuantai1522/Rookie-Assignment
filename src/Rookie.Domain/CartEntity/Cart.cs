namespace Rookie.Domain.CartEntity
{
    public class Cart
    {
        public decimal TotalPrice { get; set; }
        public List<CartItem> Items { get; set; } = [];
    }
}