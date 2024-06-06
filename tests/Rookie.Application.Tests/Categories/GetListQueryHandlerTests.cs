using Moq;
using Rookie.Application.Categories.Queries.GetListQuery;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Categories
{
    public class GetListQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsSuccessResult_WhenRequestIsValid()
        {
            // Arrange

            _mockCategoryRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(GetFakeCategories);

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<CategoryVm>>(It.IsAny<IEnumerable<Category>>()))
                .Returns(GetFakeCategoriesVm);

            var handler = new GetListQueryHandler(_mockCategoryRepository.Object, _mockMapper.Object);
            var query = new GetListQuery { };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value);
        }
        private IEnumerable<Category> GetFakeCategories()
        {
            return
            [
                new Category { Id = new CategoryId(Guid.NewGuid()), Name = "Category 1" },
                new Category { Id = new CategoryId(Guid.NewGuid()), Name = "Category 2" },
                new Category { Id = new CategoryId(Guid.NewGuid()), Name = "Category 3" },
            ];
        }
        private IEnumerable<CategoryVm> GetFakeCategoriesVm()
        {
            return
            [
                new CategoryVm { Id = "1", Name = "CategoryVm 1" },
                new CategoryVm { Id = "2", Name = "CategoryVm 2" },
                new CategoryVm { Id = "3", Name = "CategoryVm 3" },
            ];
        }
    }


}
