using AutoFixture;
using FluentAssertions;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Orders.Mappers
{
    public class OrderProfileTests : SetupProfileTest
    {
        [Fact]
        public void TestOrderVmMapper()
        {
            var orderMock = _fixture.Build<Order>()
                                    .Create();

            var result = _mapper.Map<OrderVm>(orderMock);

            result.Id.Should().Be(orderMock.Id.ToString());

        }

        [Fact]
        public void TestOrderItemVmMapper()
        {
            var orderItemMock = _fixture.Build<OrderItem>()
                                    .Create();

            var result = _mapper.Map<OrderItemVm>(orderItemMock);

            result.Id.Should().Be(orderItemMock.Id.ToString());

        }
    }
}
