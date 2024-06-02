namespace Rookie.WebApi.Controllers.Products.Request
{
    public sealed record DeleteRequest
    {
        public string? ProductId { get; set; }
    }
}