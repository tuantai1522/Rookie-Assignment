using CloudinaryDotNet.Actions;
using Moq;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Tests;
using Rookie.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Persistence.Tests.Repositories
{
    public class CategoryRepositoryTests : SetupTest
    {
        private readonly CategoryRepository _categoryRepository;


        public CategoryRepositoryTests()
        {
            _categoryRepository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public async Task GetOne_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { Id = new CategoryId("A0E40DA5-F642-46DF-A4F4-A9B6B87935AD"), Name = "Category1" };

            // Act
            var result = await _categoryRepository.GetOne(c => c.Id.Equals(new CategoryId(category.Id.ToString())), "Products");

            // Assert
            Assert.Equal(category.Id, result.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var category = new Category { Id = new CategoryId(Guid.NewGuid().ToString()), Name = "Category1" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var updatedCategory = new Category { Id = category.Id, Name = "UpdatedCategory" };

            // Act
            var result = await _categoryRepository.Update(updatedCategory);

            // Assert
            Assert.True(result);
            var updatedEntity = await _dbContext.Categories.FindAsync(category.Id);
            Assert.Equal("UpdatedCategory", updatedEntity.Name);
        }

    }
}
