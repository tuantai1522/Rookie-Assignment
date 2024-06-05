using Microsoft.AspNetCore.Identity;
using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Application.Users.Extensions
{
    public static class UserExtension
    {
        public static async Task<IQueryable<ApplicationUser>> FilterByRolesAsync(this IQueryable<ApplicationUser> query,
                                                                                UserManager<ApplicationUser> userManager,
                                                                                string roles)
        {
            var roleList = new List<string>();

            if (!string.IsNullOrEmpty(roles))
                roleList.AddRange(roles.Split(",").Select(r => r.Trim()));

            if (roleList.Count == 0)
                return query;

            var usersWithRoles = new List<ApplicationUser>();

            foreach (var role in roleList)
            {
                var roleUsers = await userManager.GetUsersInRoleAsync(role);
                usersWithRoles.AddRange(roleUsers);
            }

            var userIdsWithRoles = usersWithRoles.Select(u => u.Id).ToList();

            return query.Where(user => userIdsWithRoles.Contains(user.Id));
        }
    }
}