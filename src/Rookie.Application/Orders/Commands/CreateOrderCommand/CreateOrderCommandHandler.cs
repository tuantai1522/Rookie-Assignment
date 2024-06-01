using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Orders.Commands.CreateOrderCommand
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderId>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartService _cartService;

        public CreateOrderCommandHandler(UserManager<ApplicationUser> userManager,
                                         IOrderRepository orderRepository,
                                         IProductRepository productRepository,
                                         ICartService cartService)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _cartService = cartService;
        }

        public async Task<Result<OrderId>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<OrderId>(OrderErrors.CreateInvalidData);


            var user = await _userManager
                            .Users
                            .FirstOrDefaultAsync(u => u.UserName.Equals(request.UserName));

            //can not find user
            if (user is null)
                return Result.Failure<OrderId>(OrderErrors.NotFindUser);


            //cart is empty
            var cart = await _cartService.GetCart(request.UserName);

            if (cart.CartItems.Count == 0)
                return Result.Failure<OrderId>(OrderErrors.CartEmpty);


            List<OrderItem> items = new List<OrderItem>();

            foreach (var item in cart.CartItems)
            {
                var product = await _productRepository.GetOne(x => x.Id.Equals(new ProductId(item.ProductId)));
                var orderItem = new OrderItem()
                {
                    ProductId = new ProductId(item.ProductId),
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                };

                items.Add(orderItem);
            }
            var SubTotal = items.Sum(item => item.UnitPrice * item.Quantity);
            var DeliveryFee = SubTotal > 1000 ? 0 : 20;

            var order = new Order()
            {
                OrderItems = items,
                UserId = user.Id,
                DeliveryFee = DeliveryFee,
                SubTotal = SubTotal,
                ShippingAddress = request.ShippingAddress,
            };

            _orderRepository.Add(order);
            // await _cartService.ClearCart(request.UserName);


            return order.Id;

        }
    }
}