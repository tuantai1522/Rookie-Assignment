namespace Rookie.WebApi.Controllers.Categories.Request
{
    public sealed record DeleteRequest
    {
        public string? CategoryId { get; set; }

    }
}