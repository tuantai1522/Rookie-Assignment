using Microsoft.AspNetCore.Identity;

namespace Rookie.Domain.ApplicationUserEntity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<ApplicationUserAddress>? ApplicationUserAddresses { get; set; } = [];
    }
}