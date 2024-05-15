using MediatR;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;
using Result = Rookie.Domain.Common.Result;

namespace Rookie.Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductId>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<ProductId>(ProductErrors.CreateProductInvalidData);

            //check whether this category exists or not
            var category = await _categoryRepository.GetOne(x => x.Id.Equals(new CategoryId(request.CategoryId)));

            if (category == null)
                return Result.Failure<ProductId>(ProductErrors.NotFindCategory);

            var NewProduct = new Product();
            NewProduct.ProductName = request.ProductName;
            NewProduct.Description = request.Description;
            NewProduct.Price = request.Price;
            NewProduct.Images = request.Images;
            NewProduct.CategoryId = new CategoryId(request.CategoryId);

            _productRepository.Add(NewProduct);

            return NewProduct.Id;
        }
    }
}