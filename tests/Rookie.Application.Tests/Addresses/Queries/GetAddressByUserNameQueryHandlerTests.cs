using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Addresses.Queries.GetAddressByUserNameQuery;
using Rookie.Application.Addresses.ViewModels;
using Rookie.Application.Users.Commands.RegisterCommand;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Addresses.Queries
{
    public class GetAddressByUserNameQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<GetAddressByUserNameQuery>()
                                  .With(x => x.UserName, "")
                                  .Create();

            var handler = _fixture.Create<GetAddressByUserNameQueryHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().Be(true);
            result.Error.Should().Be(AddressError.NotEnoughInfo);

        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<GetAddressByUserNameQuery>()
                                  .With(x => x.UserName, Guid.NewGuid().ToString())
                                  .Create();

            // Set up the mock repository to return null when user is not found
            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), "ApplicationUserAddresses"))
                .ReturnsAsync(null as ApplicationUser);

            var handler = new GetAddressByUserNameQueryHandler(
                _mockUserRepository.Object,
                _mockAddressRepository.Object,
                _mockMapper.Object);



            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue(); // Ensure failure result
            result.Error.Should().Be(AddressError.NotFindUser);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenAddressIsFound()
        {
            // Arrange
            var request = _fixture.Build<GetAddressByUserNameQuery>()
                                  .With(x => x.UserName, Guid.NewGuid().ToString())
                                  .Create();

            var handler = _fixture.Create<GetAddressByUserNameQueryHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }
    }
}
