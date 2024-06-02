namespace Rookie.WebApi.Controllers.Categories.Request
{
    public sealed record CreateRequest
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}