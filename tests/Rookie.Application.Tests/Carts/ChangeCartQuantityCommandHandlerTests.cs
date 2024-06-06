using Moq;
using Rookie.Application.Carts.Commands.ChangeCartQuantityCommand;
using Rookie.Application.Carts.Queries.GetCartByUserNameQuery;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Carts
{
    public class ChangeCartQuantityCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new ChangeCartQuantityCommand
            {
                UserName = "",
            };

            var handler = new ChangeCartQuantityCommandHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockProductRepository.Object
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
            var request = new ChangeCartQuantityCommand
            {
                UserName = Guid.NewGuid().ToString(),
                ProductId = Guid.NewGuid().ToString(),
                Quantity = 3,
            };

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var handler = new ChangeCartQuantityCommandHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockProductRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CartErrors.CanNotFindUser, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenProductIsNotFound()
        {
            // Arrange
            var request = new ChangeCartQuantityCommand
            {
                UserName = Guid.NewGuid().ToString(),
                ProductId = Guid.NewGuid().ToString(),
                Quantity = 3,
            };

            var user = new ApplicationUser()
            {
                UserName = Guid.NewGuid().ToString(),
            };
            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((Product)null);

            var handler = new ChangeCartQuantityCommandHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockProductRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CartErrors.CanNotFindProduct, result.Error);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCartIsChanged()
        {
            // Arrange
            var request = new ChangeCartQuantityCommand
            {
                UserName = Guid.NewGuid().ToString(),
                ProductId = Guid.NewGuid().ToString(),
                Quantity = 3,
            };

            var user = new ApplicationUser()
            {
                UserName = Guid.NewGuid().ToString(),
            };
            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var product = new Product()
            {
                Id = new ProductId(Guid.NewGuid().ToString())
            };
            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(product);

            var handler = new ChangeCartQuantityCommandHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockProductRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }


    }
}
