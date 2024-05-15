using AutoMapper;
using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                throw new Exception();

            var ProductUpdated = new Product
            {
                Id = request.Id,
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                Images = request.Images,
                CategoryId = request.CategoryId,
            };

            await _productRepository.Update(ProductUpdated);

            // map data from Course to CourseVm wich is defined in Mappers
            return _mapper.Map<Product, ProductVm>(ProductUpdated);
        }
    }
}