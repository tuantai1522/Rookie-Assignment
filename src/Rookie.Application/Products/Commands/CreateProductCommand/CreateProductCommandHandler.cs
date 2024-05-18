using MediatR;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Images.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.MainImageEntity;
using Rookie.Domain.ProductEntity;
using Result = Rookie.Domain.Common.Result;

namespace Rookie.Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductId>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        private readonly IMainImageRepository _mainImageRepository;
        public CreateProductCommandHandler(IProductRepository productRepository,
                                            ICategoryRepository categoryRepository,
                                            IImageService imageService,
                                            IImageRepository imageRepository,
                                            IMainImageRepository mainImageRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
            _mainImageRepository = mainImageRepository;
        }
        public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid and don't upload image
            if (validationResult.IsValid == false)
                return Result.Failure<ProductId>(ProductErrors.CreateProductInvalidData);

            //check whether this category exists or not
            var category = await _categoryRepository.GetOne(x => x.Id.Equals(new CategoryId(request.CategoryId)));
            if (category == null)
                return Result.Failure<ProductId>(ProductErrors.NotFindCategory);

            //upload image on cloud
            ImageVm imageDto = await _imageService.AddPhoto(request.FileImage);

            if (imageDto.Url.Equals(""))
                return Result.Failure<ProductId>(ProductErrors.UploadImageFailed);

            //upload product on local
            var NewProduct = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock,
                CategoryId = new CategoryId(request.CategoryId)
            };
            _productRepository.Add(NewProduct);

            //upload image on local
            var NewImage = new Image
            {
                ProductId = NewProduct.Id,
                Url = imageDto.Url,
                PublicId = imageDto.PublicId,
            };

            _imageRepository.Add(NewImage);

            //upload main image on local
            var NewMainImage = new MainImage
            {
                ProductId = NewProduct.Id,
                ImageId = NewImage.Id,
            };

            _mainImageRepository.Add(NewMainImage);

            return NewProduct.Id;
        }
    }
}