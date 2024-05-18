using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.MainImageEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Persistence.Configurations
{
    public class MainImageConfiguration : BaseConfiguration<MainImage>
    {
        public override void Configure(EntityTypeBuilder<MainImage> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.ProductId);

            builder.Property(x => x.ProductId)
                    .HasConversion(x => x.Value, x => new ProductId(x));

            builder.Property(x => x.ImageId)
                    .HasConversion(x => x.Value, x => new ImageId(x));

            builder.HasOne(mi => mi.Image)
                    .WithMany()
                    .HasForeignKey(mi => mi.ImageId)
                    .OnDelete(DeleteBehavior.Cascade);


        }
    }
}