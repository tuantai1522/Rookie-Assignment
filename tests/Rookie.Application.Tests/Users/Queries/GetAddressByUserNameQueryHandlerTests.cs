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

namespace Rookie.Application.Tests.Users.Queries
{
    public class GetAddressByUserNameQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new GetAddressByUserNameQuery
            {
                UserName = "",
            };

            var handler = new GetAddressByUserNameQueryHandler(
                _mockUserRepository.Object,
                _mockAddressRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotEnoughInfo, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserIsNotFound()
        {
            // Arrange
            var request = new GetAddressByUserNameQuery
            {
                UserName = Guid.NewGuid().ToString(),
            };

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), "ApplicationUserAddresses"))
                .ReturnsAsync((ApplicationUser)null);

            var handler = new GetAddressByUserNameQueryHandler(
                _mockUserRepository.Object,
                _mockAddressRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotCorrectInfo, result.Error);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenAddressIsNotFound()
        {
            // Arrange
            var request = new GetAddressByUserNameQuery
            {
                UserName = Guid.NewGuid().ToString(),
            };

            var user = new ApplicationUser()
            {
                UserName = Guid.NewGuid().ToString(),
            };
            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), "ApplicationUserAddresses"))
                .ReturnsAsync(user);

            var listUserAddress = new List<ApplicationUserAddressVm> { };
            _mockMapper.Setup(mapper => mapper.Map<ICollection<ApplicationUserAddressVm>>(user.ApplicationUserAddresses))
                .Returns(listUserAddress);

            var handler = new GetAddressByUserNameQueryHandler(
                _mockUserRepository.Object,
                _mockAddressRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
    }
}
