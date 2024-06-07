using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Orders.Commands.CreateOrderCommand;
using Rookie.Application.Orders.Queries.GetByIdQuery;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Orders.Queries
{
    public class GetByIdQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
                                .With(x => x.UserName, "")
                                .Create();

            var handler = _fixture.Create<GetByIdQueryHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(OrderErrors.NotProvidingId);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUserIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
                                .With(x => x.OrderId, Guid.NewGuid().ToString())
                                .Create();

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as ApplicationUser);

            var handler = new GetByIdQueryHandler(
                _mockOrderRepository.Object,
                _mockMapper.Object,
                _mockUserRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(OrderErrors.NotFindUser);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenOrderIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
                                .With(x => x.OrderId, Guid.NewGuid().ToString())
                                .Create();

            var user = _fixture.Build<ApplicationUser>()
                    .Create();

            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var handler = new GetByIdQueryHandler(
                _mockOrderRepository.Object,
                _mockMapper.Object,
                _mockUserRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(OrderErrors.NotFindOrder);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenOrderIsFound()
        {
            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
                                  .With(r => r.OrderId, Guid.NewGuid().ToString())
                                  .Create();

            var order = _fixture.Build<Order>()
                                .With(o => o.Id, new OrderId(request.OrderId))
                                .Create();
            _mockOrderRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<string>()))
                                .ReturnsAsync(order);

            var orderVm = _fixture.Build<OrderVm>()
                      .Create();
            _mockMapper.Setup(m => m.Map<Order, OrderVm>(It.IsAny<Order>()))
                       .Returns(orderVm);

            var user = _fixture.Build<ApplicationUser>()
                               .With(u => u.Orders, new List<Order> { order })
                               .Create();
            _mockUserRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<string>()))
                               .ReturnsAsync(user);

            var handler = new GetByIdQueryHandler(
                _mockOrderRepository.Object,
                _mockMapper.Object,
                _mockUserRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
    }
}
