using Moq;
using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Categories.Commands.UpdateCategoryCommand;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Tests.Categories
{
    public class UpdateCategoryCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new UpdateCategoryCommand
            {
                CategoryName = ""
            };

            var handler = new UpdateCategoryCommandHandler(
                _mockCategoryRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CategoryErrors.UpdateCategoryInvalidData, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryIsNotUpdated()
        {
            // Arrange
            var request = new UpdateCategoryCommand
            {
                Id = Guid.NewGuid().ToString(),
                CategoryName = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            };

            _mockCategoryRepository.Setup(repo => repo.Update(It.IsAny<Category>()))
                .ReturnsAsync(false);

            var handler = new UpdateCategoryCommandHandler(
                _mockCategoryRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CategoryErrors.NotFindCategory, result.Error);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCategoryIsUpdated()
        {
            // Arrange
            var request = new UpdateCategoryCommand
            {
                Id = Guid.NewGuid().ToString(),
                CategoryName = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            _mockCategoryRepository.Setup(repo => repo.Update(It.IsAny<Category>()))
                .ReturnsAsync(true);

            var category = new Category
            {
                Id = new CategoryId(request.Id),
                Name = request.CategoryName,
                Description = request.Description
            };

            var categoryVm = new CategoryVm
            {
                Id = request.Id,
                Name = request.CategoryName,
                Description = request.Description
            };

            _mockMapper.Setup(m => m.Map<Category, CategoryVm>(It.IsAny<Category>()))
                .Returns(categoryVm);

            var handler = new UpdateCategoryCommandHandler(
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
