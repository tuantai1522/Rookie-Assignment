using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Addresses.Queries.GetAddressByUserNameQuery;
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

namespace Rookie.Application.Tests.Carts.Commands
{
    public class ChangeCartQuantityCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<ChangeCartQuantityCommand>()
                      .With(x => x.UserName, "")
                      .Create();


            var handler = _fixture.Create<ChangeCartQuantityCommandHandler>();
            
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CartErrors.ChangeCartQuantityInvalidData);

        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<ChangeCartQuantityCommand>()
                            .Create();

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as ApplicationUser);

            var handler = new ChangeCartQuantityCommandHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockProductRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CartErrors.CanNotFindUser);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenProductIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<ChangeCartQuantityCommand>()
                            .Create();

            var user = new ApplicationUser()
            {
                UserName = Guid.NewGuid().ToString(),
            };
            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as Product);

            var handler = new ChangeCartQuantityCommandHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockProductRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CartErrors.CanNotFindProduct);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCartIsChanged()
        {
            // Arrange
            var request = _fixture.Build<ChangeCartQuantityCommand>()
                            .Create();

            var handler = _fixture.Create<ChangeCartQuantityCommandHandler>();


            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().Be(1);
        }


    }
}
