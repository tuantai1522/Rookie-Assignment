namespace Rookie.WebApi.Controllers.Carts.Request
{
    public class ChangeCartQuantityRequest
    {
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}