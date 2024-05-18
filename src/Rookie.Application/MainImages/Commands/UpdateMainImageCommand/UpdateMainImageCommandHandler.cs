using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.MainImages.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.MainImageEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.MainImages.Commands.UpdateMainImageCommand
{
    public class UpdateMainImageCommandHandler : IRequestHandler<UpdateMainImageCommand, Result<MainImageVm>>
    {
        private readonly IMainImageRepository _mainImageRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public UpdateMainImageCommandHandler(IMainImageRepository mainImageRepository,
                                             IImageRepository imageRepository,
                                             IMapper mapper)
        {
            _mainImageRepository = mainImageRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<Result<MainImageVm>> Handle(UpdateMainImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMainImageCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<MainImageVm>(MainImageErrors.UpdateMainImageFailed);

            //unknown image
            var CurImage = await _imageRepository.GetOne(x => x.Id.Equals(new ImageId(request.ImageId)));
            if (CurImage == null)
                return Result.Failure<MainImageVm>(MainImageErrors.NotFindImage);

            //to check whether this image belongs to this product or not
            var check = CurImage.ProductId.Equals(new ProductId(request.ProductId));
            if (check == false)
                return Result.Failure<MainImageVm>(MainImageErrors.InvalidImage);


            var MainImageUpdated = new MainImage
            {
                ImageId = new ImageId(request.ImageId),
                ProductId = new ProductId(request.ProductId),
            };

            var temp = await _mainImageRepository.Update(MainImageUpdated);

            if (temp == true)
                // map data from Product to ProductVm wich is defined in Mappers
                return _mapper.Map<MainImage, MainImageVm>(MainImageUpdated);
            else
                return Result.Failure<MainImageVm>(MainImageErrors.NotFindProduct);
        }
    }
}