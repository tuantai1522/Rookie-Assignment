using Moq;
using Rookie.Application.Products.Commands.CreateProductCommand;
using Rookie.Application.Products.Commands.DeleteProductCommand;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Image = Rookie.Domain.ImageEntity.Image;

namespace Rookie.Application.Tests.Products.Commands
{
    public class DeleteProductCommandTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new DeleteProductCommand
            {
                ProductId = "123"
            };

            var handler = new DeleteProductCommandHandler(
                _mockProductRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.DeleteProductInvalidData, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenProductIsNotFound()
        {
            // Arrange
            var request = new DeleteProductCommand
            {
                ProductId = Guid.NewGuid().ToString(),
            };

            var handler = new DeleteProductCommandHandler(
                _mockProductRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NotFindProduct, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenImageIsNotFound()
        {
            // Arrange
            var request = new DeleteProductCommand
            {
                ProductId = Guid.NewGuid().ToString(),
            };

            var handler = new DeleteProductCommandHandler(
                _mockProductRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object
            );

            var Product = new Product()
            {
                Id = new ProductId(request.ProductId),
            };
            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(Product);

            _mockImageRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Image, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Image>());

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NotFindImage, result.Error);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenProductIsDeleted()
        {
            // Arrange
            var request = new DeleteProductCommand
            {
                ProductId = Guid.NewGuid().ToString(),
            };

            var handler = new DeleteProductCommandHandler(
                _mockProductRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object
            );

            var Product = new Product()
            {
                Id = new ProductId(request.ProductId),
            };
            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(Product);

            var Image = new Image()
            {
                Id = new ImageId(Guid.NewGuid().ToString())
            };
            _mockImageRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Image, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Image>([Image]));

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Value);
        }
    }
}
