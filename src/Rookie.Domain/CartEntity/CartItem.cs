namespace Rookie.Domain.CartEntity
{
    public class CartItem
    {
        public CartItemId Id { get; set; } = new CartItemId(Guid.NewGuid());
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductImage { get; set; }
        public decimal TotalPrice { get; set; }
    }
}