using Moq;
using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Products.Commands.CreateProductCommand;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Categories
{
    public class CreateCategoryCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new CreateCategoryCommand
            {
                CategoryName = ""
            }; 

            var handler = new CreateCategoryCommandHandler(
                _mockCategoryRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CategoryErrors.CreateCategoryInvalidData, result.Error);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCategoryIsCreated()
        {
            // Arrange
            var request = new CreateCategoryCommand
            {
                CategoryName = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            };

            _mockCategoryRepository.Setup(repo => repo.Add(It.IsAny<Category>()));

            var handler = new CreateCategoryCommandHandler(
                _mockCategoryRepository.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
    }
}
