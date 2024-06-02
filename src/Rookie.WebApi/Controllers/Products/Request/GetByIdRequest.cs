namespace Rookie.WebApi.Controllers.Products.Request
{
    public sealed record GetByIdRequest
    {
        public string? ProductId { get; set; }
    }
}