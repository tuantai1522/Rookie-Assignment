using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.OrderEntity;

namespace Rookie.Persistence.Configurations
{
    public class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id)
                    .HasConversion(x => x.Value, x => new OrderId(x));

            //One order belongs to one ApplicationUser
            builder.HasOne(p => p.ApplicationUser)
                .WithMany(c => c.Orders)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}