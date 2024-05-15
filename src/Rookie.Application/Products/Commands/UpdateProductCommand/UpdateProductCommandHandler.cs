using AutoMapper;
using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductVm>>
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository,
                                           ICategoryRepository categoryRepository,
                                           IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProductVm>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<ProductVm>(ProductErrors.UpdateProductInvalidData);

            var category = await _categoryRepository.GetOne(x => x.Id.Equals(request.CategoryId));
            if (category == null)
                return Result.Failure<ProductVm>(ProductErrors.NotFindCategory);

            var ProductUpdated = new Product
            {
                Id = request.Id,
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                Images = request.Images,
                CategoryId = request.CategoryId,
            };

            var temp = await _productRepository.Update(ProductUpdated);

            if (temp == true)
                // map data from Product to ProductVm wich is defined in Mappers
                return _mapper.Map<Product, ProductVm>(ProductUpdated);
            else
                return Result.Failure<ProductVm>(ProductErrors.NotFindProduct);

        }
    }
}