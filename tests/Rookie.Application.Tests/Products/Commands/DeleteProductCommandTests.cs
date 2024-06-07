using AutoFixture;
using FluentAssertions;
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
            var request = _fixture.Build<DeleteProductCommand>()
              .With(r => r.ProductId, "")
              .Create();

            var handler = _fixture.Create<DeleteProductCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.DeleteProductInvalidData);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenProductIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<DeleteProductCommand>()
                .With(r => r.ProductId, Guid.NewGuid().ToString())
                .Create();

            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as Product);

            var handler = new DeleteProductCommandHandler(
                _mockProductRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.NotFindProduct);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenImageIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<DeleteProductCommand>()
                .With(r => r.ProductId, Guid.NewGuid().ToString())
                .Create();

            var product = _fixture.Build<Product>()
                .With(r => r.Id, new ProductId(request.ProductId))
                .Create();

            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(product);

            _mockImageRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Image, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Image>());

            var handler = new DeleteProductCommandHandler(
                _mockProductRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.NotFindImage);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenProductIsDeleted()
        {
            // Arrange
            var request = _fixture.Build<DeleteProductCommand>()
                .With(r => r.ProductId, Guid.NewGuid().ToString())
                .Create();

            var product = _fixture.Build<Product>()
                .With(r => r.Id, new ProductId(request.ProductId))
                .Create();

            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(product);


            var image = _fixture.Build<Image>()
                .With(r => r.Id, new ImageId(Guid.NewGuid().ToString()))
                .Create();

            _mockImageRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Image, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Image>([image]));

            var handler = new DeleteProductCommandHandler(
                _mockProductRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().Be(1);
        }
    }
}
