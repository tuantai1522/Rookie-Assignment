using MediatR;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                throw new Exception();

            var ProductDeleted = await _productRepository.GetOne(x => x.Id.Equals(request.ProductId));

            if (ProductDeleted == null)
                throw new Exception();

            _productRepository.Delete(ProductDeleted);

            return 1;
        }
    }
}