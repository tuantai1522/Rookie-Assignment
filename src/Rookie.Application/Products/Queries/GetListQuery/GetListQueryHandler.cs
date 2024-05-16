using AutoMapper;
using MediatR;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<PagedList<Product>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public async Task<Result<PagedList<Product>>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetListQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<PagedList<Product>>(ProductErrors.QueryProductInvalidData);

            var products = await _productRepository.GetAll(request.ProductParams, includeProperties: "Category");

            return Result.Success(products);
        }


    }

}