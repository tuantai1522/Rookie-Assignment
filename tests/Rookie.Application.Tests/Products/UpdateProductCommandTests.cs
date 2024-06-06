using Microsoft.AspNetCore.Http;
using Moq;
using Rookie.Application.Products.Commands.UpdateProductCommand;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Products
{
    public class UpdateProductCommandTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new UpdateProductCommand
            {
                CategoryId = Guid.NewGuid().ToString(),
                Price = 123,
            }; //Not enough field



            var handler = new UpdateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockMapper.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.UpdateProductInvalidData, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryDoesNotExist()
        {
            // Arrange
            var request = new UpdateProductCommand
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid().ToString(),
                ProductName = "Test Product",
                Description = "Test Description",
                Price = 100,
                QuantityInStock = 10,
            };

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((Category)null);

            var handler = new UpdateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockMapper.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NotFindCategory, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenProductDoesNotExist()
        {
            // Arrange
            var category = new Category()
            {
                Id = new CategoryId(Guid.NewGuid().ToString()),
                Name = "Category 1",
            };

            var request = new UpdateProductCommand
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = category.Id.ToString(),
                ProductName = "Test Product",
                Description = "Test Description",
                Price = 100,
                QuantityInStock = 10,
            };

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);

            _mockProductRepository.Setup(repo => repo.Update(It.IsAny<Product>()))
                .ReturnsAsync(false);

            var handler = new UpdateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockMapper.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NotFindProduct, result.Error);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenProductIsUpdated()
        {
            // Arrange
            var category = new Category()
            {
                Id = new CategoryId(Guid.NewGuid().ToString()),
                Name = "Category 1",
            };

            var product = new Product()
            {
                Id = new ProductId(Guid.NewGuid().ToString()),
                CategoryId = new CategoryId(category.Id.ToString()),
                ProductName = "Test Product",
                Description = "Test Description",
                Price = 100,
                QuantityInStock = 10,
            };

            var request = new UpdateProductCommand
            {
                Id = product.Id.ToString(),
                CategoryId = category.Id.ToString(),
                ProductName = "Updated Product",
                Description = "Updated Description",
                Price = 200,
                QuantityInStock = 20,
            };

            var productVm = new ProductVm
            {
                Id = request.Id,
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock,
            };

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);

            _mockProductRepository.Setup(repo => repo.Update(It.IsAny<Product>()))
                .ReturnsAsync(true);

            _mockMapper.Setup(m => m.Map<Product, ProductVm>(It.IsAny<Product>()))
                .Returns(productVm);

            var handler = new UpdateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
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
