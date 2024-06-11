using MediatR;
using Rookie.Application.Ratings.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Ratings.Commands.CreateRatingCommand
{
    public class CreateRatingCommand : IRequest<Result<RatingVm>>
    {
        public string UserName { get; set; }
        public string OrderItemId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

    }
}