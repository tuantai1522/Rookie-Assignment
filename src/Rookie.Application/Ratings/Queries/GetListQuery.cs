using MediatR;
using Rookie.Application.Ratings.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Ratings.Queries
{
    public class GetListQuery : IRequest<Result<RatingVm>>
    {
        public string ProductId { get; set; }
        public RatingParams RatingParams { get; set; }
    }
}