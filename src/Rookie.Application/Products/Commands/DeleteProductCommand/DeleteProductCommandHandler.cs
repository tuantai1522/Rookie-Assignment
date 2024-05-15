using MediatR;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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

            _productRepository.Delete(ProductDeleted);

            return 1;
        }
    }
}