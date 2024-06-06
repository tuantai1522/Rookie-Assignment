using Moq;
using Rookie.Application.Carts.Queries.GetCartByUserNameQuery;
using Rookie.Application.Carts.ViewModels;
using Rookie.Application.Products.Commands.CreateProductCommand;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CartEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Carts.Queries
{
    public class GetCartByUserNameQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new GetCartByUserNameQuery
            {
                UserName = "",
            };

            var handler = new GetCartByUserNameQueryHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CartErrors.ChangeCartQuantityInvalidData, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserIsNotFound()
        {
            // Arrange
            var request = new GetCartByUserNameQuery
            {
                UserName = Guid.NewGuid().ToString(),
            };

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var handler = new GetCartByUserNameQueryHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CartErrors.CanNotFindUser, result.Error);


        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCartIsFound()
        {
            // Arrange
            var request = new GetCartByUserNameQuery
            {
                UserName = Guid.NewGuid().ToString(),
            };

            var user = new ApplicationUser()
            {
                UserName = Guid.NewGuid().ToString(),
            };
            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var cartVm = new CartVm()
            {
                TotalPrice = 0,
                CartItems = new List<CartItemVm>()
            };
            _mockMapper.Setup(m => m.Map<Cart, CartVm>(It.IsAny<Cart>()))
                .Returns(cartVm);

            var handler = new GetCartByUserNameQueryHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

        }
    }
}
