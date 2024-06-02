namespace Rookie.WebApi.Controllers.Products.Request
{
    public sealed record CreateRequest
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int QuantityInStock { get; set; }
        public IFormFile? FileImage { get; set; }
        public decimal Price { get; set; }
        public string? CategoryId { get; set; }
    }
}