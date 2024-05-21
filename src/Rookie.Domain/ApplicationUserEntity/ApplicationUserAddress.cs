using Rookie.Domain.Common;

namespace Rookie.Domain.ApplicationUserEntity
{
    public class ApplicationUserAddress : Address
    {
        public UserAddressId Id = new UserAddressId(Guid.NewGuid());
        public string? UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}