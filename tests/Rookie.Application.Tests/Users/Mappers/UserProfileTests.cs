using AutoFixture;
using FluentAssertions;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Users.Mappers
{
    public class UserProfileTests : SetupProfileTest
    {
        [Fact]
        public void TestUserLoginVmMapper()
        {
            var userMock = _fixture.Build<ApplicationUser>()
                                    .Without(x => x.Orders)
                                    .Without(x => x.ApplicationUserAddresses)
                                    .Create();

            var result = _mapper.Map<UserLoginVm>(userMock);

            result.Id.Should().Be(userMock.Id.ToString());

        }

        [Fact]
        public void TestUserRegisterVmMapper()
        {
            var userMock = _fixture.Build<ApplicationUser>()
                                    .Without(x => x.Orders)
                                    .Without(x => x.ApplicationUserAddresses)
                                    .Create();

            var result = _mapper.Map<UserRegisterVm>(userMock);

            Assert.Equal(result.Email, userMock.Email);
            Assert.Equal(result.UserName, userMock.UserName);
            Assert.Equal(result.FirstName, userMock.FirstName);
            Assert.Equal(result.LastName, userMock.LastName);
        }

        [Fact]
        public void TestUserInfoVmMapper()
        {
            var userMock = _fixture.Build<ApplicationUser>()
                                    .Without(x => x.Orders)
                                    .Without(x => x.ApplicationUserAddresses)
                                    .Create();

            var result = _mapper.Map<UserInfoVm>(userMock);

            result.Id.Should().Be(userMock.Id.ToString());

        }
    }
}
