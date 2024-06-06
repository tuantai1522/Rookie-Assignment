using Moq;
using Rookie.Application.Categories.Queries.GetByIdQuery;
using Rookie.Application.Products.Commands.UpdateProductCommand;
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

using Rookie.Application.Categories.ViewModels;


namespace Rookie.Application.Tests.Categories.Queries
{
    public class GetByIdQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = new GetByIdQuery
            {
                Id = "",
            };

            var handler = new GetByIdQueryHandler(
                _mockCategoryRepository.Object,
                _mockMapper.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CategoryErrors.GetCategoryByIdInvalidData, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryIsNotFound()
        {
            // Arrange
            var request = new GetByIdQuery
            {
                Id = Guid.NewGuid().ToString(),
            };

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((Category)null);

            var handler = new GetByIdQueryHandler(
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
        public async Task ReturnsSuccessResult_WhenCategoryIsFound()
        {
            // Arrange
            var request = new GetByIdQuery
            {
                Id = Guid.NewGuid().ToString(),
            };

            var category = new Category()
            {
                Id = new CategoryId(request.Id),
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            };

            var categoryVm = new CategoryVm
            {
                Id = request.Id,
                Name = category.Id.ToString(),
                Description = category.Description,
            };

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);

            _mockMapper.Setup(m => m.Map<Category, CategoryVm>(It.IsAny<Category>()))
                .Returns(categoryVm);

            var handler = new GetByIdQueryHandler(
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
