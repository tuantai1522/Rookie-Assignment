using CloudinaryDotNet.Actions;
using Moq;
using FluentAssertions;
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
using AutoFixture;
using Rookie.Domain.ProductEntity;

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
            //in database
            var category = _fixture.Build<Category>()
                                .With(x => x.Id, new CategoryId("A0E40DA5-F642-46DF-A4F4-A9B6B87935AD"))
                                .Create();

            // Act
            var result = await _categoryRepository.GetOne(c => c.Id.Equals(new CategoryId(category.Id.ToString())), "Products");

            // Assert
            result.Id.Should().Be(category.Id);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListCategory()
        {
            // Arrange
            var category = _fixture.Build<Category>()
                                .Without(x => x.Products)
                                .Create();

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetAll(c => c.Id.Equals(new CategoryId(category.Id.ToString())), "Products");
            var returnedCategory = result.First();

            // Assert
            result.Should().ContainSingle();
            returnedCategory.Id.Should().Be(category.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var category = _fixture.Build<Category>()
                                .Without(x => x.Products)
                                .Create();

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var updatedCategory = _fixture.Build<Category>()
                                .Without(x => x.Products)
                                .With(x => x.Id, category.Id)
                                .Create();

            // Act
            var result = await _categoryRepository.Update(updatedCategory);
            var updatedEntity = await _dbContext.Categories.FindAsync(category.Id);

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenUpdateIsNotSuccessful()
        {
            // Arrange
            var category = _fixture.Create<Category>();

            // Act
            var result = await _categoryRepository.Update(category);
            var updatedEntity = await _dbContext.Categories.FindAsync(category.Id);

            // Assert
            result.Should().Be(false);
            updatedEntity?.Name.Should().NotBe("UpdatedCategory");
        }

    }
}
