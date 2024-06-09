using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.RatingEntity;

namespace Rookie.Persistence.Configurations
{
    public class RatingConfiguration : BaseConfiguration<Rating>
    {
        public override void Configure(EntityTypeBuilder<Rating> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id)
                    .HasConversion(x => x.Value, x => new RatingId(x));


            //One rating belongs to one ApplicationUser
            builder.HasOne(p => p.ApplicationUser)
                .WithMany(c => c.Ratings)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            //One rating belongs to one Product
            builder.HasOne(p => p.Product)
                .WithMany(c => c.Ratings)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}