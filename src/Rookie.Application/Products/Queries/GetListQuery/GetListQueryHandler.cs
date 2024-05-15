using AutoMapper;
using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<IEnumerable<ProductVm>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetListQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<IEnumerable<ProductVm>>(ProductErrors.QueryProductInvalidData);

            var products = await _productRepository.GetAll(request.ProductParams, includeProperties: "Category");

            var productVms = _mapper.Map<IEnumerable<ProductVm>>(products);

            return Result.Success(productVms);
        }


    }

}