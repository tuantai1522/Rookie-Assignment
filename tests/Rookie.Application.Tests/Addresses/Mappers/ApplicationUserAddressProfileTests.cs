using AutoFixture;
using FluentAssertions;
using Rookie.Application.Addresses.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CartEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Addresses.Mappers
{
    public class ApplicationUserAddressProfileTests : SetupProfileTest
    {
        [Fact]
        public void TestApplicationUserAddressVmMapper()
        {
            var addressMock = _fixture.Build<ApplicationUserAddress>()
                                    .Without(x => x.ApplicationUser)
                                    .Without(x => x.Address)
                                    .Create();

            var result = _mapper.Map<ApplicationUserAddressVm>(addressMock);

            result.Id.Should().Be(addressMock.Id.ToString());
        }
    }
}
