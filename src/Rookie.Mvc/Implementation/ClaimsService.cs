
using System.Security.Claims;
using Rookie.Mvc.Interface;

namespace Rookie.Mvc.Implementation
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            var role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
            GetCurrentUserRole = string.IsNullOrEmpty(role) ? string.Empty : role;

            var userName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            GetCurrentUserName = string.IsNullOrEmpty(userName) ? string.Empty : userName;
        }
        public string GetCurrentUserRole { get; }
        public string GetCurrentUserName { get; }
    }
}