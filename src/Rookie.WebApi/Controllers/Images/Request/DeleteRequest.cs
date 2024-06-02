namespace Rookie.WebApi.Controllers.Images.Request
{
    public sealed record DeleteRequest
    {
        public string? ImageId { get; set; }
    }
}