using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Ratings.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Ratings.Commands.CreateRatingCommand
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Result<RatingVm>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public CreateRatingCommandHandler(IUserRepository userRepository,
                                 IOrderRepository orderRepository,
                                 IRatingRepository ratingRepository,
                                 IMapper mapper)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<Result<RatingVm>> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRatingCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<RatingVm>(RatingErrors.CreateRatingInvalidData);


            //can not find user
            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName));
            if (user is null)
                return Result.Failure<RatingVm>(RatingErrors.NotFindUser);


            //this orderItem does not exist
            var orderItem = await _orderRepository.CheckOrderItemExists(new OrderItemId(request.OrderItemId));
            if (orderItem == false)
                return Result.Failure<RatingVm>(RatingErrors.NotFindOrderItem);

            //this orderItem was rated
            var rating = await _ratingRepository.GetOne(x => x.OrderItemId.Equals(new OrderItemId(request.OrderItemId)));
            if (rating is not null)
                return Result.Failure<RatingVm>(RatingErrors.AlreadyRated);

            var NewRating = new Rating
            {
                UserId = user.Id,
                Value = (RatingValue)request.Rating,
                Comment = request.Comment,
                OrderItemId = new OrderItemId(request.OrderItemId),
            };


            _ratingRepository.Add(NewRating);

            var ratingVm = _mapper.Map<RatingVm>(NewRating);

            return ratingVm;
        }
    }
}