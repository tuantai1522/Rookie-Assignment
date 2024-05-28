namespace Rookie.Application.Products.ViewModels
{
    public class ProductVm
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }
        public int QuantityInStock { get; set; }
        public List<string> ImageUrls { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

    }
}