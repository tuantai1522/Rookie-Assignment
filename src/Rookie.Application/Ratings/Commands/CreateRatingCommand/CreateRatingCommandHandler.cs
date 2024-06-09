using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Ratings.Commands.CreateRatingCommand
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Result<RatingId>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRatingRepository _ratingRepository;

        public CreateRatingCommandHandler(IUserRepository userRepository,
                                 IProductRepository productRepository,
                                 IRatingRepository ratingRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<Result<RatingId>> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRatingCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<RatingId>(RatingErrors.CreateRatingInvalidData);


            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName));

            //can not find user
            if (user is null)
                return Result.Failure<RatingId>(RatingErrors.NotFindUser);

            var product = await _productRepository.GetOne(x => x.Id.Equals(new ProductId(request.ProductId)));

            //can not find user
            if (product is null)
                return Result.Failure<RatingId>(RatingErrors.NotFindProduct);

            var NewRating = new Rating
            {
                ProductId = product.Id,
                UserId = user.Id,
                Value = (RatingValue)request.Rating,
                Comment = request.Comment,
            };


            _ratingRepository.Add(NewRating);
            return NewRating.Id;
        }
    }
}