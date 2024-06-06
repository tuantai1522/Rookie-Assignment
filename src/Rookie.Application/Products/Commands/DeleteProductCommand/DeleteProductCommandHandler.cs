using MediatR;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;
        public DeleteProductCommandHandler(IProductRepository productRepository,
                                           IImageService imageService,
                                           IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }
        public async Task<Result<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<int>(ProductErrors.DeleteProductInvalidData);

            var ProductDeleted = await _productRepository.GetOne(x => x.Id.Equals(new ProductId(request.ProductId)));

            if (ProductDeleted == null)
                return Result.Failure<int>(ProductErrors.NotFindProduct);

            //find all images related to this product 
            var images = await _imageRepository.GetAll(x => x.ProductId.Equals(new ProductId(request.ProductId)));

            if (images == null || !images.Any()) // Also check if the list is empty
                return Result.Failure<int>(ProductErrors.NotFindImage);

            //delete on local
            _productRepository.Delete(ProductDeleted);


            //delete on cloud
            foreach (var image in images)
                await _imageService.DeletePhoto(image.PublicId);
            
            return 1;
        }
    }
}