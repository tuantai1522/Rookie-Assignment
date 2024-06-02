namespace Rookie.WebApi.Controllers.Carts.Request
{
    public sealed record ChangeCartQuantityRequest
    {
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}