using Rookie.Domain.Common;

namespace Rookie.Application.Contracts.Infrastructure
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(
            string id,
            string firstName,
            string lastName,
            string userName,
            string email,
            List<string> roles);
    }
}