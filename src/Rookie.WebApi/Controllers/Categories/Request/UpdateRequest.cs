namespace Rookie.WebApi.Controllers.Categories.Request
{
    public sealed record UpdateRequest
    {
        public string? Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}