using Microsoft.AspNetCore.Identity;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.RatingEntity;

namespace Rookie.Domain.ApplicationUserEntity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<ApplicationUserAddress>? ApplicationUserAddresses { get; set; } = [];
        public ICollection<Order>? Orders { get; set; } = [];
        public ICollection<Rating>? Ratings { get; set; } = [];

    }
}