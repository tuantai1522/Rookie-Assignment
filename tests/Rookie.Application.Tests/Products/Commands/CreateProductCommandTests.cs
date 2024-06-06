using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Images.ViewModels;
using Rookie.Application.Products.Commands.CreateProductCommand;
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

namespace Rookie.Application.Tests.Products.Commands
{
    public class CreateProductCommandTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new CreateProductCommand
            {
                CategoryId = Guid.NewGuid().ToString(),
                Price = 123,
            }; //Not enough field



            var handler = new CreateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object,
                _mockMainImageRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.CreateProductInvalidData, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryDoesNotExist()
        {
            // Arrange
            var request = new CreateProductCommand
            {
                CategoryId = Guid.NewGuid().ToString(), // Use any GUID for testing
                ProductName = "Test Product",
                Description = "Test Description",
                Price = 100,
                QuantityInStock = 10,
                FileImage = new Mock<IFormFile>().Object // Mock or create a sample IFormFile
            };

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((Category)null);

            var handler = new CreateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object,
                _mockMainImageRepository.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NotFindCategory, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUploadImageFailed()
        {
            // Arrange
            var category = new Category
            {
                Id = new CategoryId(Guid.NewGuid()),
                Name = "Category 1"
            };


            var request = new CreateProductCommand
            {
                CategoryId = category.Id.ToString(),
                ProductName = "Test Product",
                Description = "Test Description",
                Price = 100,
                QuantityInStock = 10,
                FileImage = new Mock<IFormFile>().Object // Mock or create a sample IFormFile
            };

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);

            ImageVm imageVm = new ImageVm()
            {
                Url = "",
                PublicId = "",
            };
            _mockImageService.Setup(service => service.AddPhoto(It.IsAny<IFormFile>()))
                .ReturnsAsync(imageVm);

            var handler = new CreateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object,
                _mockMainImageRepository.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.UploadImageFailed, result.Error);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenProductIsCreated()
        {
            // Arrange
            var category = new Category
            {
                Id = new CategoryId(Guid.NewGuid()),
                Name = "Category 1"
            };


            var request = new CreateProductCommand
            {
                CategoryId = category.Id.ToString(),
                ProductName = "Test Product",
                Description = "Test Description",
                Price = 100,
                QuantityInStock = 10,
                FileImage = new Mock<IFormFile>().Object
            };

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);

            ImageVm imageVm = new ImageVm()
            {
                Url = Guid.NewGuid().ToString(),
                PublicId = Guid.NewGuid().ToString(),
            };
            _mockImageService.Setup(service => service.AddPhoto(It.IsAny<IFormFile>()))
                .ReturnsAsync(imageVm);

            var handler = new CreateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object,
                _mockMainImageRepository.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
    }
}
