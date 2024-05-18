using MediatR;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Images.ViewModels;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Images.Commands.CreateImageCommand
{
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, Result<ImageId>>
    {
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        private readonly IProductRepository _productRepository;

        public CreateImageCommandHandler(IImageService imageService,
                                         IImageRepository imageRepository,
                                         IProductRepository productRepository)
        {
            _imageService = imageService;
            _imageRepository = imageRepository;
            _productRepository = productRepository;
        }
        public async Task<Result<ImageId>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateImageCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid and don't upload image
            if (validationResult.IsValid == false)
                return Result.Failure<ImageId>(ImageErrors.CreateImageInvalidData);

            //check whether this category exists or not
            var product = await _productRepository.GetOne(x => x.Id.Equals(new ProductId(request.ProductId)));
            if (product == null)
                return Result.Failure<ImageId>(ImageErrors.NotFindProduct);

            //upload image on cloud
            ImageVm imageDto = await _imageService.AddPhoto(request.FileImage);

            if (imageDto.Url.Equals(""))
                return Result.Failure<ImageId>(ImageErrors.UploadImageFailed);

            //upload image on local
            var NewImage = new Image
            {
                ProductId = new ProductId(request.ProductId),
                Url = imageDto.Url,
                PublicId = imageDto.PublicId,
            };

            _imageRepository.Add(NewImage);

            return NewImage.Id;
        }
    }
}