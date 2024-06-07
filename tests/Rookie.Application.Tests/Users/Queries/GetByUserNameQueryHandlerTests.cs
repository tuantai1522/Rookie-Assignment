using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Users.Commands.LoginCommand;
using Rookie.Application.Users.Commands.RegisterCommand;
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

namespace Rookie.Application.Tests.Users.Queries
{
    public class GetByUserNameQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<GetByUserNameQuery>()
                        .With(x => x.UserName, "")
                        .Create();


            var handler = _fixture.Create<GetByUserNameQueryHandler>();


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
            var request = _fixture.Create<GetByUserNameQuery>();


            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as ApplicationUser);

            var handler = new GetByUserNameQueryHandler(
                _mockUserRepository.Object,
                _mockMapper.Object,
                _mockJwtTokenGenerator.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(UserErrors.NotCorrectInfo);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenUserIsFound()
        {
            // Arrange
            var request = _fixture.Create<GetByUserNameQuery>();
            var user = _fixture.Create<ApplicationUser>();


            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var roles = new string[] { "Role1", "Role2" };
            _mockUserRepository.Setup(repo => repo.GetRoles(user))
                .ReturnsAsync(roles);


            var userLoginVm = _fixture.Create<UserLoginVm>();

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

            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }
    }
}
