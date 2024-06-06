using Moq;
using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Categories.Commands.DeleteCategoryCommand;
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

namespace Rookie.Application.Tests.Categories.Commands
{
    public class DeleteCategoryCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new DeleteCategoryCommand
            {
                CategoryId = "",
            };

            var handler = new DeleteCategoryCommandHandler(
                _mockCategoryRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CategoryErrors.DeleteCategoryInvalidData, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryIsNotFound()
        {
            // Arrange
            var request = new DeleteCategoryCommand
            {
                CategoryId = Guid.NewGuid().ToString(),
            };


            var handler = new DeleteCategoryCommandHandler(
                _mockCategoryRepository.Object
            );

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((Category)null);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CategoryErrors.NotFindCategory, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryIsDeleted()
        {
            // Arrange
            var request = new DeleteCategoryCommand
            {
                CategoryId = Guid.NewGuid().ToString(),
            };


            var handler = new DeleteCategoryCommandHandler(
                _mockCategoryRepository.Object
            );

            var category = new Category()
            {
                Id = new CategoryId(request.CategoryId),
            };
            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);

            _mockCategoryRepository.Setup(repo => repo.Delete(It.IsAny<Category>()));

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Value);
        }
    }
}


