namespace Rookie.WebApi.Controllers.MainImages.Request
{
    public sealed record UpdateRequest
    {
        public string? ProductId { get; set; }
        public string? ImageId { get; set; }
    }
}