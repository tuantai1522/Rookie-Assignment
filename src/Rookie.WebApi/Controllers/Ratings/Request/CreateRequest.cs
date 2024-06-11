namespace Rookie.WebApi.Controllers.Ratings.Request
{
    public sealed record CreateRequest
    {
        public string? OrderItemId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
    }
}