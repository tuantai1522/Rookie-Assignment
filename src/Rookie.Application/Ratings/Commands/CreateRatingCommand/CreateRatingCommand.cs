using MediatR;
using Rookie.Domain.Common;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Ratings.Commands.CreateRatingCommand
{
    public class CreateRatingCommand : IRequest<Result<RatingId>>
    {
        public string UserName { get; set; }
        public string ProductId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

    }
}