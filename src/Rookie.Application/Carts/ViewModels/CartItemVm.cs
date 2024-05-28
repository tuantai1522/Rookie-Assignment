namespace Rookie.Application.Carts.ViewModels
{
    public class CartItemVm
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public decimal TotalPrice { get; set; }
    }
}