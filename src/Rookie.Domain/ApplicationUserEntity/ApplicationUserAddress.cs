using System.Text.Json.Serialization;
using Rookie.Domain.Common;

namespace Rookie.Domain.ApplicationUserEntity
{
    public class ApplicationUserAddress : BaseEntity
    {
        public UserAddressId Id = new UserAddressId(Guid.NewGuid());
        public string? UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? ApplicationUser { get; set; }
        public Address? Address { get; set; }
    }
}