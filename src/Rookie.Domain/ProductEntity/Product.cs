using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;

namespace Rookie.Domain.ProductEntity
{
    public class Product : BaseEntity
    {
        public ProductId Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? Images { get; set; }
        public decimal Price { get; set; }

        public Category? Category { get; set; }
    }
}