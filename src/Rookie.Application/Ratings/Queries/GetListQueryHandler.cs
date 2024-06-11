using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Ratings.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Ratings.Queries
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<RatingVm>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IRatingRepository _ratingRepository;

        public GetListQueryHandler(IProductRepository productRepository,
                                    IRatingRepository ratingRepository)
        {
            this._productRepository = productRepository;
            this._ratingRepository = ratingRepository;
        }


        public async Task<Result<RatingVm>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetListQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<RatingVm>(RatingErrors.QueryRatingInvalidData);


            var product = await _productRepository.GetOne(x => x.Id.Equals(new ProductId(request.ProductId)));

            if (product is null)
                return Result.Failure<RatingVm>(RatingErrors.NotFindProduct);


            var ratings = await _ratingRepository.GetRatingBasedOnProduct(x => x.OrderItem.Product.Id.Equals(new ProductId(request.ProductId)),
                                                                        request.RatingParams,
                                                                        "OrderItem,ApplicationUser");

            // Paginate UserNames and Comments
            var userNames = ratings.Select(x => x.ApplicationUser.UserName)
                                   .Skip((request.RatingParams.PageNumber - 1) * request.RatingParams.PageSize)
                                   .Take(request.RatingParams.PageSize)
                                   .ToList();


            var comments = ratings.Select(x => x.Comment ?? string.Empty)
                                  .Skip((request.RatingParams.PageNumber - 1) * request.RatingParams.PageSize)
                                  .Take(request.RatingParams.PageSize)
                                  .ToList();


            var ratingVm = new RatingVm()
            {
                ProductName = product.ProductName,
                Rating = ratings.Average(x => (double)x.Value),
                Comments = comments,
                UserNames = userNames,
            };

            return Result.Success(ratingVm);
        }
    }
}