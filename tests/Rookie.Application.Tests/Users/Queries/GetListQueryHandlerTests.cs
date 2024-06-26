﻿using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Users.Queries.GetListQuery;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Users.Queries
{
    public class GetListQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFaliureResult_WhenRequestIsInvalid()
        {
            // Arrange
            var applicationUserParams = new ApplicationUserParams { PageNumber = -1, PageSize = 6 };


            var request = _fixture.Build<GetListQuery>()
                .With(x => x.ApplicationUserParams, applicationUserParams)
                .Create();

            _mockUserRepository.Setup(repo => repo.GetAll(It.IsAny<ApplicationUserParams>(), It.IsAny<string>()))
                .ReturnsAsync(new PagedList<ApplicationUser>(GetFakeUsers(), GetFakeUsers().Count, -1, 6));

            _mockMapper.Setup(mapper => mapper.Map<PagedList<UserInfoVm>>(It.IsAny<PagedList<Product>>()))
                .Returns(new PagedList<UserInfoVm>(GetFakeUserInfoVms(), GetFakeUserInfoVms().Count, -1, 6));

            var handler = new GetListQueryHandler(_mockUserRepository.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().NotBeNull();
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenRequestIsValid()
        {
            // Arrange
            var applicationUserParams = new ApplicationUserParams { PageNumber = 1, PageSize = 6 };

            var request = _fixture.Build<GetListQuery>()
                .With(x => x.ApplicationUserParams, applicationUserParams)
                .Create();

            _mockUserRepository.Setup(repo => repo.GetAll(It.IsAny<ApplicationUserParams>(), It.IsAny<string>()))
                .ReturnsAsync(new PagedList<ApplicationUser>(GetFakeUsers(), GetFakeUsers().Count, 1, 6));

            _mockMapper.Setup(mapper => mapper.Map<PagedList<UserInfoVm>>(It.IsAny<PagedList<ApplicationUser>>()))
                .Returns(new PagedList<UserInfoVm>(GetFakeUserInfoVms(), GetFakeUserInfoVms().Count, 1, 6));

            var handler = new GetListQueryHandler(_mockUserRepository.Object, _mockMapper.Object);


            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
            result.Value.Should().NotBeEmpty();
        }

        private List<ApplicationUser> GetFakeUsers()
        {
            return
            [
                new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "UserName 1" },
                new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "UserName 2" },
                new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "UserName 3" },
            ];
        }
        private List<UserInfoVm> GetFakeUserInfoVms()
        {
            return
            [
                new UserInfoVm { Id = "1", UserName = "UserInfoVm 1" },
                new UserInfoVm { Id = "2", UserName = "UserInfoVm 2" },
                new UserInfoVm { Id = "3", UserName = "UserInfoVm 3" },
            ];
        }
    }
}
