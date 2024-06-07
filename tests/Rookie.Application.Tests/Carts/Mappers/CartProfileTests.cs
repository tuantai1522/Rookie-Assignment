using AutoFixture;
using Rookie.Application.Carts.ViewModels;
using Rookie.Domain.CartEntity;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Carts.Mappers
{
    public class CartProfileTests : SetupProfileTest
    {
        [Fact]
        public void TestCartVmMapper()
        {
            var cartMock = _fixture.Build<Cart>()
                                    .Create();

            var result = _mapper.Map<CartVm>(cartMock);

            Assert.Equal(result.TotalPrice, cartMock.TotalPrice);
            Assert.Equal(result.CartItems.Count, cartMock.CartItems.Count);
        }
    }
}
