using AutoMapper;
using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;
using Rookie.Application.Contracts.Persistence;

namespace Rookie.Application.Products.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<PagedList<ProductVm>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public async Task<Result<PagedList<ProductVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetListQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<PagedList<ProductVm>>(ProductErrors.QueryProductInvalidData);

            var products = await _productRepository.GetAll(request.ProductParams, "Category,MainImage,Images");

            var productVms = _mapper.Map<PagedList<ProductVm>>(products);

            return Result.Success(productVms);
        }
    }

}