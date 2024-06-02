namespace Rookie.WebApi.Controllers.Users.Request
{
    public sealed record LoginUserRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}