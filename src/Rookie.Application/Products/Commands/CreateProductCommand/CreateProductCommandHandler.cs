using AutoMapper;
using MediatR;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductId>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductId> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                throw new Exception();

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