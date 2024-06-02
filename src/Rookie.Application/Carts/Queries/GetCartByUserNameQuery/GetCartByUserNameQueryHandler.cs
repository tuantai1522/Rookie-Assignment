using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Carts.ViewModels;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CartEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Carts.Queries.GetCartByUserNameQuery
{
    public class GetCartByUserNameQueryHandler : IRequestHandler<GetCartByUserNameQuery, Result<CartVm>>
    {
        private readonly ICartService _cartService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public GetCartByUserNameQueryHandler(ICartService cartService,
                                    IUserRepository userRepository,
                                    IMapper mapper)
        {
            _cartService = cartService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<CartVm>> Handle(GetCartByUserNameQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCartByUserNameValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid and don't upload image
            if (validationResult.IsValid == false)
                return Result.Failure<CartVm>(CartErrors.ChangeCartQuantityInvalidData);

            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName));

            //can not find user
            if (user is null)
                return Result.Failure<CartVm>(CartErrors.CanNotFindUser);

            var cart = await _cartService.GetCart(user.UserName);

            return _mapper.Map<Cart, CartVm>(cart);

        }
    }
}