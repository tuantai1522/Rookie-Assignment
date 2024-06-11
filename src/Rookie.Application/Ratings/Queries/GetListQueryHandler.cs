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
    public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<PagedList<RatingVm>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IProductRepository productRepository,
                                   IRatingRepository ratingRepository,
                                   IMapper mapper)
        {
            this._productRepository = productRepository;
            this._ratingRepository = ratingRepository;
            _mapper = mapper;
        }


        public async Task<Result<PagedList<RatingVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetListQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<PagedList<RatingVm>>(RatingErrors.QueryRatingInvalidData);


            var product = await _productRepository.GetOne(x => x.Id.Equals(new ProductId(request.ProductId)));

            if (product is null)
                return Result.Failure<PagedList<RatingVm>>(RatingErrors.NotFindProduct);


            var ratings = await _ratingRepository.GetRatingBasedOnProduct(x => x.OrderItem.Product.Id.Equals(new ProductId(request.ProductId)),
                                                                        request.RatingParams,
                                                                        "OrderItem,ApplicationUser");

            var ratingVms = _mapper.Map<PagedList<RatingVm>>(ratings);

            return Result.Success(ratingVms);
        }
    }
}