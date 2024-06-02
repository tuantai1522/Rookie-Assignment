namespace Rookie.WebApi.Controllers.Products.Request
{
    public sealed record UpdateRequest
    {
        public string? Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public string? CategoryId { get; set; }
    }
}