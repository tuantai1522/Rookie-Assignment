using Rookie.Domain.Common;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Domain.MainImageEntity
{
    public class MainImage : BaseEntity
    {
        public ProductId? ProductId { get; set; }
        public Product? Product { get; set; }

        public ImageId? ImageId { get; set; }
        public Image? Image { get; set; }
    }
}