using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.RatingEntity;

namespace Rookie.Persistence.Configurations
{
    public class RatingConfiguration : BaseConfiguration<Rating>
    {
        public override void Configure(EntityTypeBuilder<Rating> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.OrderItemId);

            builder.Property(x => x.OrderItemId)
                    .HasConversion(x => x.Value, x => new OrderItemId(x));

            //One rating belongs to one ApplicationUser
            builder.HasOne(p => p.ApplicationUser)
                .WithMany(c => c.Ratings)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}