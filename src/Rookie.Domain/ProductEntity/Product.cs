using System.Text.Json.Serialization;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.MainImageEntity;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.RatingEntity;

namespace Rookie.Domain.ProductEntity
{
    public class Product : BaseEntity
    {
        public ProductId Id { get; set; } = new ProductId(Guid.NewGuid());
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public CategoryId? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

        public ICollection<Image>? Images { get; set; } = [];
        public ICollection<OrderItem>? OrderItems { get; set; } = [];
        public ICollection<Rating>? Ratings { get; set; } = [];

        public MainImage? MainImage { get; set; }
    }
}