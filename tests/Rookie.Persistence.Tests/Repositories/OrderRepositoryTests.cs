using AutoFixture;
using Azure.Core;
using FluentAssertions;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using Rookie.Persistence.Repositories;

namespace Rookie.Persistence.Tests.Repositories
{
    public class OrderRepositoryTests : SetupTest
    {
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryTests()
        {
            _orderRepository = new OrderRepository(_dbContext);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOrder()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                                .Without(x => x.ApplicationUserAddresses)
                                .Without(x => x.Orders)
                                .Create();

            _dbContext.Users.Add(user);

            var order = _fixture.Build<Order>()
                                .Without(x => x.ApplicationUser)
                                .Without(x => x.OrderItems)
                                .With(x => x.UserId, user.Id)
                                .With(x => x.ApplicationUser, user)
                                .Create();
            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();

            var orderParams = new OrderParams { PageNumber = 1, PageSize = 1 };

            // Act
            var result = await _orderRepository.GetAll(orderParams, "ApplicationUser");

            // Assert
            result.Should().ContainSingle();
        }

        [Fact]
        public async Task GetAll_ShouldReturnListById()
        {
            var user = _fixture.Build<ApplicationUser>()
                            .Without(x => x.ApplicationUserAddresses)
                            .Without(x => x.Orders) // Exclude Orders collection initially
                            .Create();
            _dbContext.Users.Add(user);

            var category = _fixture.Build<Category>()
                    .Without(x => x.Products)
                    .Create();
            _dbContext.Categories.Add(category);

            var product = _fixture.Build<Product>()
                                  .Without(x => x.Images)
                                  .Without(x => x.MainImage)
                                  .Without(x => x.OrderItems)
                                  .With(x => x.Category, category)
                                  .With(x => x.CategoryId, category.Id)
                                  .Create();
            _dbContext.Products.Add(product);

            var orderItem = _fixture.Build<OrderItem>()
                                .Without(x => x.Product)
                                .With(x => x.Product, product)
                                .With(x => x.ProductId, product.Id)
                                .Create();

            var order = _fixture.Build<Order>()
                            .With(o => o.OrderItems, new[] { orderItem }) // Specify the OrderItem
                            .Create();

            user.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            var orderParams = new OrderParams { PageNumber = 1, PageSize = 1 };

            // Act
            var result = await _orderRepository.GetListById(x => x.ApplicationUser!.UserName!.Equals(user.UserName),
                                                            orderParams,
                                                            "ApplicationUser");

            result.Should().ContainSingle();
        }

        [Fact]
        public async Task GetOne_ShouldReturnOrder()
        {
            // Arrange
            var user = _fixture.Build<ApplicationUser>()
                                .Without(x => x.ApplicationUserAddresses)
                                .Without(x => x.Orders)
                                .Create();

            _dbContext.Users.Add(user);

            var order = _fixture.Build<Order>()
                                .Without(x => x.ApplicationUser)
                                .Without(x => x.OrderItems)
                                .With(x => x.UserId, user.Id)
                                .With(x => x.ApplicationUser, user)
                                .Create();
            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();


            // Act
            var result = await _orderRepository.GetOne(x => x.ApplicationUser.UserName!.Equals(user.UserName), "ApplicationUser");

            // Assert
            result.Id.Should().Be(order.Id);
        }
    }
}
