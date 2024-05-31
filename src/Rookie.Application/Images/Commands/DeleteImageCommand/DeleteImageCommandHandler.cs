using MediatR;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Images.Commands.DeleteImageCommand
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;

        public DeleteImageCommandHandler(IProductRepository productRepository,
                                   IImageService imageService,
                                   IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }

        public async Task<Result<int>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteImageCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<int>(ImageErrors.DeleteImageInvalidData);

            //to check image exists or not
            var ImageDeleted = await _imageRepository.GetOne(x => x.Id.Equals(new ImageId(request.ImageId)));

            if (ImageDeleted == null)
                return Result.Failure<int>(ImageErrors.NotFindImage);

            //to check this image is main image or not
            var Product = await _productRepository.GetOne(x => x.MainImage.ImageId.Equals(new ImageId(request.ImageId)));

            if (Product != null)
                return Result.Failure<int>(ImageErrors.InvalidMainImage);

            //delete on local
            _imageRepository.Delete(ImageDeleted);

            //delete on cloud
            await _imageService.DeletePhoto(ImageDeleted.PublicId);

            return 1;
        }
    }
}