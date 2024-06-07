using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Orders.Commands.CreateOrderCommand;
using Rookie.Domain.DomainError;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rookie.Domain.Common;
using AutoFixture;
using Rookie.Domain.CategoryEntity;
using Moq;
using Rookie.Domain.ApplicationUserEntity;
using System.Linq.Expressions;
using Rookie.Domain.CartEntity;
using Rookie.Domain.ProductEntity;
using FluentAssertions;
namespace Rookie.Application.Tests.Orders.Commands
{
    public class CreateOrderCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<CreateOrderCommand>()
                                    .With(x => x.UserName, "")
                                    .Create();

            var handler = _fixture.Create<CreateOrderCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(OrderErrors.CreateInvalidData);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<CreateOrderCommand>()
                                    .Create();

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as ApplicationUser);

            var handler = new CreateOrderCommandHandler(
                _mockUserRepository.Object,
                _mockOrderRepository.Object,
                _mockProductRepository.Object,
                _mockCartService.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(OrderErrors.NotFindUser);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCartIsEmpty()
        {
            // Arrange
            var request = _fixture.Build<CreateOrderCommand>()
                                    .Create();

            var user = _fixture.Build<ApplicationUser>()
                                    .Create();

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var cart = _fixture.Build<Cart>()
                                    .With(c => c.CartItems, new List<CartItem>())
                                    .Create();

            _mockCartService.Setup(repo => repo.GetCart(It.IsAny<string>()))
                            .ReturnsAsync(cart);

            var handler = new CreateOrderCommandHandler(
                _mockUserRepository.Object,
                _mockOrderRepository.Object,
                _mockProductRepository.Object,
                _mockCartService.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(OrderErrors.CartEmpty);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenOrderIsMade()
        {
            // Arrange
            var request = _fixture.Build<CreateOrderCommand>().Create();

            var user = _fixture.Build<ApplicationUser>().Create();

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var cartItems = _fixture.Build<CartItem>()
                .With(x => x.ProductId, Guid.NewGuid().ToString())
                .CreateMany(3)
                .ToList();

            var cart = _fixture.Build<Cart>()
                .With(c => c.CartItems, cartItems)
                .Create();

            _mockCartService.Setup(repo => repo.GetCart(It.IsAny<string>()))
                .ReturnsAsync(cart);

            foreach (var cartItem in cartItems)
            {
                var product = _fixture.Build<Product>()
                    .With(p => p.Id, new ProductId(cartItem.ProductId))
                    .Create();

                _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(product);
            }

            var handler = new CreateOrderCommandHandler(
                _mockUserRepository.Object,
                _mockOrderRepository.Object,
                _mockProductRepository.Object,
                _mockCartService.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }
    }
}
