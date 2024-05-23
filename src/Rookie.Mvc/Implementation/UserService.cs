using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Rookie.Application.Users.ViewModels;
using Rookie.Mvc.Interface;

namespace Rookie.Mvc.Implementation
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string IsUserLoggedIn()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWT");
            return token;
        }

        public UserLoginVm GetUserInfo(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            string GetClaimValue(string claimType)
            {
                var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);
                return claim?.Value;
            }

            return new UserLoginVm
            {
                Id = GetClaimValue("id"),
                FirstName = GetClaimValue("firstName"),
                LastName = GetClaimValue("lastName"),
                UserName = GetClaimValue("userName"),
                Email = GetClaimValue("email"),
                Token = token
            };
        }
    }
}