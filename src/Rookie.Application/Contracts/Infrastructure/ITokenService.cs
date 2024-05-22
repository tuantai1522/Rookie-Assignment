using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Application.Contracts.Infrastructure
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}