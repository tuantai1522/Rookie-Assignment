using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Carts.Commands.ChangeCartQuantityCommand;
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
            var request = _fixture.Build<GetCartByUserNameQuery>()
              .With(x => x.UserName, "")
              .Create();

            var handler = _fixture.Create<GetCartByUserNameQueryHandler>();

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
            var request = _fixture.Build<GetCartByUserNameQuery>()
              .With(x => x.UserName, Guid.NewGuid().ToString())
              .Create();

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as ApplicationUser);

            var handler = new GetCartByUserNameQueryHandler(
                _mockCartService.Object,
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CartErrors.CanNotFindUser);


        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCartIsFound()
        {
            // Arrange
            var request = _fixture.Build<GetCartByUserNameQuery>()
              .Create();

            var handler = _fixture.Create<GetCartByUserNameQueryHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);

        }
    }
}
