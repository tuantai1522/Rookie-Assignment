using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Rookie.Application.Users.Commands.LoginCommand;
using Rookie.Application.Users.Commands.RegisterCommand;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
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
    public class RegisterCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<RegisterCommand>()
                            .With(x => x.UserName, "")
                            .Create();

            var handler = _fixture.Create<RegisterCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(UserErrors.NotEnoughInfo);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenEmailExists()
        {
            // Arrange
            var request = _fixture.Create<RegisterCommand>();

            _mockUserRepository.Setup(repo => repo.CheckEmailExisted(It.IsAny<string>()))
                .Returns(true);

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(UserErrors.EmailExisted);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserNameExists()
        {
            // Arrange
            var request = _fixture.Create<RegisterCommand>();

            _mockUserRepository.Setup(repo => repo.CheckEmailExisted(It.IsAny<string>()))
                .Returns(false);

            _mockUserRepository.Setup(repo => repo.CheckUserNameExisted(It.IsAny<string>()))
                .Returns(true);

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(UserErrors.UserNameExisted);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserIsCreatedFailed()
        {
            // Arrange
            var request = _fixture.Create<RegisterCommand>();

            _mockUserRepository.Setup(repo => repo.CheckEmailExisted(It.IsAny<string>()))
                .Returns(false);

            _mockUserRepository.Setup(repo => repo.CheckUserNameExisted(It.IsAny<string>()))
                .Returns(false);

            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Create user failed" });
            _mockUserRepository.Setup(repo => repo.CreateUser(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert

            result.IsSuccess.Should().Be(false);
            "User.RegisterError".Should().Be(UserErrors.CreateCustomRegisterError(string.Join(". ",
                                        identityResult.Errors.Select(e => e.Description))));
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCustomerIsCreatedSuccessfully()
        {
            // Arrange
            var request = _fixture.Build<RegisterCommand>()
                                .With(x => x.Role, "")
                                .Create();

            _mockUserRepository.Setup(repo => repo.CheckEmailExisted(It.IsAny<string>()))
                .Returns(false);

            _mockUserRepository.Setup(repo => repo.CheckUserNameExisted(It.IsAny<string>()))
                .Returns(false);

            var identityResult = IdentityResult.Success;
            _mockUserRepository.Setup(repo => repo.CreateUser(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);


            var user = _fixture.Create<ApplicationUser>();
            var userRegisterVm = _fixture.Create<UserRegisterVm>();

            _mockMapper.Setup(mapper => mapper.Map<UserRegisterVm>(It.IsAny<ApplicationUser>()))
                .Returns(userRegisterVm);

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();

        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenAdminIsCreatedSuccessfully()
        {
            // Arrange
            var request = _fixture.Create<RegisterCommand>();

            _mockUserRepository.Setup(repo => repo.CheckEmailExisted(It.IsAny<string>()))
                .Returns(false);

            _mockUserRepository.Setup(repo => repo.CheckUserNameExisted(It.IsAny<string>()))
                .Returns(false);

            var identityResult = IdentityResult.Success;
            _mockUserRepository.Setup(repo => repo.CreateUser(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            var user = _fixture.Create<ApplicationUser>();
            var userRegisterVm = _fixture.Create<UserRegisterVm>();

            _mockMapper.Setup(mapper => mapper.Map<UserRegisterVm>(It.IsAny<ApplicationUser>()))
                .Returns(userRegisterVm);

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }
    }
}
