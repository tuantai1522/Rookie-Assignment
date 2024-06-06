﻿using Moq;
using Rookie.Application.Users.Commands.LoginCommand;
using Rookie.Application.Users.Queries.GetByUserNameQuery;
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
    public class GetByUserNameQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new GetByUserNameQuery
            {
                UserName = "",
            };

            var handler = new GetByUserNameQueryHandler(
                _mockUserRepository.Object,
                _mockMapper.Object,
                _mockJwtTokenGenerator.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotEnoughInfo, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenInfoIsNotCorrect()
        {
            // Arrange
            var request = new GetByUserNameQuery
            {
                UserName = Guid.NewGuid().ToString(),
            };


            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var handler = new GetByUserNameQueryHandler(
                _mockUserRepository.Object,
                _mockMapper.Object,
                _mockJwtTokenGenerator.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotCorrectInfo, result.Error);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenUserIsFound()
        {
            // Arrange
            var request = new GetByUserNameQuery
            {
                UserName = Guid.NewGuid().ToString(),
            };

            var user = new ApplicationUser()
            {
                UserName = request.UserName,
            };
            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var roles = new string[] { "Role1", "Role2" };
            _mockUserRepository.Setup(repo => repo.GetRoles(user))
                .ReturnsAsync(roles);


            var userLoginVm = new UserLoginVm()
            {
                UserName = user.UserName,
            };
            _mockMapper.Setup(mapper => mapper.Map<UserLoginVm>(user))
                .Returns(userLoginVm);

            var handler = new GetByUserNameQueryHandler(
                _mockUserRepository.Object,
                _mockMapper.Object,
                _mockJwtTokenGenerator.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

        }



    }
}