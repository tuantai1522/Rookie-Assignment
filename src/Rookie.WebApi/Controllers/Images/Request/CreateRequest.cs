namespace Rookie.WebApi.Controllers.Images.Request
{
    public sealed record CreateRequest
    {
        public string? ProductId { get; set; }
        public IFormFile? FileImage { get; set; }
    }
}