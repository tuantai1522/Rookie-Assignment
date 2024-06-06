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

namespace Rookie.Application.Tests.Users
{
    public class RegisterCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new RegisterCommand
            {
                UserName = "",
            };

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotEnoughInfo, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenEmailExists()
        {
            // Arrange
            var request = new RegisterCommand
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
            };

            _mockUserRepository.Setup(repo => repo.CheckEmailExisted(It.IsAny<string>()))
                .Returns(true);

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.EmailExisted, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserNameExists()
        {
            // Arrange
            var request = new RegisterCommand
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
            };

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
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.UserNameExisted, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserIsCreatedFailed()
        {
            // Arrange
            var request = new RegisterCommand
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
            };

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
            Assert.False(result.IsSuccess);

            Assert.Equal("User.RegisterError", UserErrors.CreateCustomRegisterError(string.Join(". ", 
                                        identityResult.Errors.Select(e => e.Description))));
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCustomerIsCreatedSuccessfully()
        {
            // Arrange
            var request = new RegisterCommand
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
            };

            _mockUserRepository.Setup(repo => repo.CheckEmailExisted(It.IsAny<string>()))
                .Returns(false);

            _mockUserRepository.Setup(repo => repo.CheckUserNameExisted(It.IsAny<string>()))
                .Returns(false);

            var identityResult = IdentityResult.Success;
            _mockUserRepository.Setup(repo => repo.CreateUser(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            var user = new ApplicationUser()
            {
                UserName = Guid.NewGuid().ToString(),
            };

            var userRegisterVm = new UserRegisterVm()
            {
                UserName = user.UserName,
            };
            _mockMapper.Setup(mapper => mapper.Map<UserRegisterVm>(It.IsAny<ApplicationUser>()))
                .Returns(userRegisterVm);

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenAdminIsCreatedSuccessfully()
        {
            // Arrange
            var request = new RegisterCommand
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                Role = Guid.NewGuid().ToString(),
            };

            _mockUserRepository.Setup(repo => repo.CheckEmailExisted(It.IsAny<string>()))
                .Returns(false);

            _mockUserRepository.Setup(repo => repo.CheckUserNameExisted(It.IsAny<string>()))
                .Returns(false);

            var identityResult = IdentityResult.Success;
            _mockUserRepository.Setup(repo => repo.CreateUser(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            var user = new ApplicationUser()
            {
                UserName = Guid.NewGuid().ToString(),
            };

            var userRegisterVm = new UserRegisterVm()
            {
                UserName = user.UserName,
            };
            _mockMapper.Setup(mapper => mapper.Map<UserRegisterVm>(It.IsAny<ApplicationUser>()))
                .Returns(userRegisterVm);

            var handler = new RegisterCommandHandler(
                _mockUserRepository.Object,
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
