using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Persistence.Configurations
{
    public class ApplicationUserAddressConfiguration : BaseConfiguration<ApplicationUserAddress>
    {
        public override void Configure(EntityTypeBuilder<ApplicationUserAddress> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id)
                    .HasConversion(x => x.Value, x => new UserAddressId(x));

            //One Address only belongs to One User
            builder.HasOne(p => p.ApplicationUser)
                .WithMany(c => c.ApplicationUserAddresses)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}