using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Domain.CategoryEntity
{
    public class Category : BaseEntity
    {
        public CategoryId Id { get; set; } = new(Guid.NewGuid());
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; } = [];
    }
}