using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Domain.ImageEntity
{
    public class Image : BaseEntity
    {
        public ImageId Id { get; set; } = new ImageId(Guid.NewGuid());
        public string? Url { get; set; }
        public string? PublicId { get; set; }

        public ProductId? ProductId { get; set; }
        public Product? Product { get; set; }


    }
}