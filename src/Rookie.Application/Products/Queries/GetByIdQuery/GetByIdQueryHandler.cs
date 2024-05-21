using AutoMapper;
using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;


namespace Rookie.Application.Products.Queries.GetByIdQuery
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<ProductVm>>
    {
        private readonly IProductRepository _productCategory;

        private readonly IMapper _mapper;
        public GetByIdQueryHandler(IProductRepository productCategory, IMapper mapper)
        {
            _productCategory = productCategory;
            _mapper = mapper;
        }
        public async Task<Result<ProductVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetByIdQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<ProductVm>(ProductErrors.QueryProductInvalidData);

            var product = await _productCategory.GetOne(x => x.Id.Equals(new ProductId(request.ProductId)), "Category,MainImage,Images");

            if (product != null)
                // map data from Course to CourseVm wich is defined in Mappers
                return _mapper.Map<Product, ProductVm>(product);
            else
                return Result.Failure<ProductVm>(ProductErrors.NotFindProduct);

        }
    }
}