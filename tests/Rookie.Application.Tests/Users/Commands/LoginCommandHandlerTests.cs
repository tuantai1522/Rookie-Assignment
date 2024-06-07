using AutoFixture;
using CloudinaryDotNet.Actions;
using FluentAssertions;
using Moq;
using Rookie.Application.Carts.Queries.GetCartByUserNameQuery;
using Rookie.Application.Products.Commands.CreateProductCommand;
using Rookie.Application.Users.Commands.LoginCommand;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CartEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Users.Commands
{
    public class LoginCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<LoginCommand>()
                            .With(x => x.UserName, "")
                            .Create();

            var handler = _fixture.Create<LoginCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(UserErrors.NotEnoughInfo);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenInfoIsNotCorrect()
        {
            // Arrange
            var request = _fixture.Create<LoginCommand>();

            var handler = _fixture.Create<LoginCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(UserErrors.NotCorrectInfo);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenLoginSuccessfully()
        {
            // Arrange
            var request = _fixture.Create<LoginCommand>();

            var user = _fixture.Create<ApplicationUser>();


            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.CheckPasswordValid(user, request.Password))
                .ReturnsAsync(true);

            var roles = new List<string> { "User" };
            _mockUserRepository.Setup(repo => repo.GetRoles(user))
                .ReturnsAsync(roles);

            var userLoginVm = new UserLoginVm()
            {
                UserName = user.UserName,
            };
            _mockMapper.Setup(mapper => mapper.Map<UserLoginVm>(user))
                .Returns(userLoginVm);

            var handler = new LoginCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object,
                _mockJwtTokenGenerator.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }
    }
}
