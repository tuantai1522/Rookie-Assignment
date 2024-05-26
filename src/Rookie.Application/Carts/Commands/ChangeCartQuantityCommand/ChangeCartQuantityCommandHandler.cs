using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CartEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Carts.Commands.ChangeCartQuantityCommand
{
    public class ChangeCartQuantityCommandHandler : IRequestHandler<ChangeCartQuantityCommand, Result<Dictionary<string, int>>>
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductRepository _productRepository;

        public ChangeCartQuantityCommandHandler(ICartService cartService,
                                    UserManager<ApplicationUser> userManager,
                                    IProductRepository productRepository)
        {
            _cartService = cartService;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        public async Task<Result<Dictionary<string, int>>> Handle(ChangeCartQuantityCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangeCartQuantityCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid and don't upload image
            if (validationResult.IsValid == false)
                return Result.Failure<Dictionary<string, int>>(CartErrors.ChangeCartQuantityInvalidData);


            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName.Equals(request.UserName));

            //can not find user
            if (user is null)
                return Result.Failure<Dictionary<string, int>>(CartErrors.CanNotFindUser);

            var product = await _productRepository
                            .GetOne(x => x.Id == new ProductId(request.ProductId));

            //can not find product
            if (product is null)
                return Result.Failure<Dictionary<string, int>>(CartErrors.CanNotFindProduct);

            Dictionary<string, int> updatedCart = await _cartService.ChangeCartQuantity(
                                                        request.UserName,
                                                        request.ProductId,
                                                        request.Quantity);

            return updatedCart;
        }
    }
}