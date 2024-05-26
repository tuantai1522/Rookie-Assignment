namespace Rookie.Domain.CartEntity
{
    public class CartItem
    {
        public CartItemId Id { get; set; } = new CartItemId(Guid.NewGuid());
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? ProductImage { get; set; }
        public decimal TotalPrice { get; set; }
    }
}