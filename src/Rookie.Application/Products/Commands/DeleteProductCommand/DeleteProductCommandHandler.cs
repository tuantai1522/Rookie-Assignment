using MediatR;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;
        public DeleteProductCommandHandler(IProductRepository productRepository, IImageService imageService)
        {
            _productRepository = productRepository;
            _imageService = imageService;
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

            //delete on cloud
            int checkDelete = await _imageService.DeletePhoto("pyfx6fidzml09degawh8");

            //delete on cloud successfully
            if (checkDelete == 1)
            {
                //delete on local
                _productRepository.Delete(ProductDeleted);
                return 1;
            }
            else
                return 0;
        }
    }
}