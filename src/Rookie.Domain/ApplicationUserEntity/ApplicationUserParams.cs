using Rookie.Domain.Common;

namespace Rookie.Domain.ApplicationUserEntity
{
    public class ApplicationUserParams : PaginationParams
    {
        public string? Role { get; set; }
    }
}