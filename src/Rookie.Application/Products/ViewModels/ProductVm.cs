using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.ViewModels
{
    public class ProductVm
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> ImageUrls { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

        public ProductVm() { }

        public ProductVm(string id, string productName, string description, string mainImageUrl, List<string> imageUrls, decimal price, string categoryName)
        {
            Id = id;
            ProductName = productName;
            Description = description;
            MainImageUrl = mainImageUrl;
            ImageUrls = imageUrls;
            Price = price;
            CategoryName = categoryName;
        }
    }
}