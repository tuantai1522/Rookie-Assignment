using Microsoft.AspNetCore.Identity;

namespace Rookie.Domain.ApplicationUserEntity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name) : base(name) => Id = new(Guid.NewGuid().ToString());

        public ApplicationRole()
        {
        }
    }
}