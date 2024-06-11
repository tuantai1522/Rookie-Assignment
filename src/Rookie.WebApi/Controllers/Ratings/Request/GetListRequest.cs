using Rookie.Domain.RatingEntity;

namespace Rookie.WebApi.Controllers.Ratings.Request
{
    public sealed record GetListRequest
    {
        public string? ProductId { get; set; }
        public RatingParams? RatingParams { get; set; } = new RatingParams();

    }
}