using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Carts.Commands.ChangeCartQuantityCommand
{
    public class ChangeCartQuantityCommandHandler : IRequestHandler<ChangeCartQuantityCommand, Result<int>>
    {
        private readonly ICartService _cartService;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public ChangeCartQuantityCommandHandler(ICartService cartService,
                                    IUserRepository userRepository,
                                    IProductRepository productRepository)
        {
            _cartService = cartService;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<int>> Handle(ChangeCartQuantityCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangeCartQuantityCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid and don't upload image
            if (validationResult.IsValid == false)
                return Result.Failure<int>(CartErrors.ChangeCartQuantityInvalidData);

            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName));

            //can not find user
            if (user is null)
                return Result.Failure<int>(CartErrors.CanNotFindUser);

            var product = await _productRepository
                            .GetOne(x => x.Id == new ProductId(request.ProductId));

            //can not find product
            if (product is null)
                return Result.Failure<int>(CartErrors.CanNotFindProduct);

            await _cartService.ChangeCartQuantity(user.UserName,
                                                  request.ProductId,
                                                  request.Quantity);

            return 1;
        }
    }
}