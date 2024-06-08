using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.ImageEntity;
using Rookie.Domain.MainImageEntity;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using Rookie.Persistence.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Persistence.Tests.Repositories
{
    public class ProductRepositoryTests : SetupTest
    {
        private readonly ProductRepository _productRepository;


        public ProductRepositoryTests()
        {
            _productRepository = new ProductRepository(_dbContext);
        }

        [Fact]
        public async Task GetOne_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange

            //in database
            var product = _fixture.Build<Product>()
                                .With(x => x.Id, new ProductId("8CFA19E3-D235-4FBD-B118-2FFD17BE059F"))
                                .Create();
            // Act
            var result = await _productRepository.GetOne(c => c.Id.Equals(new ProductId(product.Id.ToString())), "Category");

            // Assert
            result.Id.Should().Be(product.Id);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListProduct()
        {
            // Arrange
            var category = _fixture.Build<Category>()
                                .Without(x => x.Products)
                                .Create();
            _dbContext.Categories.Add(category);

            var productId = Guid.NewGuid().ToString(); // Retrieve the saved ProductId
            var product = _fixture.Build<Product>()
                                  .Without(x => x.Images)
                                  .Without(x => x.MainImage)
                                  .Without(x => x.OrderItems)
                                  .With(x => x.Category, category)
                                  .With(x => x.CategoryId, category.Id)
                                  .With(x => x.Id, new ProductId(productId))
                                  .Create();
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            var productParams = new ProductParams { PageNumber = 1, PageSize = 1 };

            // Act
            var result = await _productRepository.GetAll(productParams, "Category");

            // Assert
            result.Should().ContainSingle();
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var category = _fixture.Build<Category>()
                                .Without(x => x.Products)
                                .Create();
            _dbContext.Categories.Add(category);

            var productId = Guid.NewGuid().ToString(); // Retrieve the saved ProductId
            var product = _fixture.Build<Product>()
                                  .Without(x => x.Images)
                                  .Without(x => x.MainImage)
                                  .Without(x => x.OrderItems)
                                  .With(x => x.Category, category)
                                  .With(x => x.CategoryId, category.Id)
                                  .Create();
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            var updatedProduct = _fixture.Build<Product>()
                                     .Without(x => x.Images)
                                     .Without(x => x.MainImage)
                                     .Without(x => x.OrderItems)
                                     .With(x => x.Category, category)
                                     .With(x => x.CategoryId, category.Id)
                                     .With(x => x.Id, product.Id)
                                     .Create();
            // Act
            var result = await _productRepository.Update(updatedProduct);
            var updatedEntity = await _dbContext.Products.FindAsync(product.Id);

            // Assert
            result.Should().Be(true);

        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenUpdateIsNotSuccessful()
        {
            // Arrange
            var category = _fixture.Create<Category>();
            var product = _fixture.Create<Product>();

            // Act
            var result = await _productRepository.Update(product);
            var updatedEntity = await _dbContext.Products.FindAsync(product.Id);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task Add_WhenInsertionIsSuccessful()
        {
            var category = _fixture.Build<Category>()
                                .Without(x => x.Products)
                                .Create();
            
            var product = _fixture.Build<Product>()
                      .Without(x => x.Images)
                      .Without(x => x.MainImage)
                      .Without(x => x.OrderItems)
                      .With(x => x.Category, category)
                      .With(x => x.CategoryId, category.Id)
                      .Create();

            _productRepository.Add(product);

            var ProductAdded = await _productRepository.GetOne(x => x.Id.Equals(product.Id));

            ProductAdded.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_WhenDeletionIsSuccessful()
        {
            var category = _fixture.Build<Category>()
                                .Without(x => x.Products)
                                .Create();

            var product = _fixture.Build<Product>()
                      .Without(x => x.Images)
                      .Without(x => x.MainImage)
                      .Without(x => x.OrderItems)
                      .With(x => x.Category, category)
                      .With(x => x.CategoryId, category.Id)
                      .Create();

            _productRepository.Add(product);
            await _dbContext.SaveChangesAsync();

            _productRepository.Delete(product);
            await _dbContext.SaveChangesAsync();

            var ProductAdded = await _productRepository.GetOne(x => x.Id.Equals(product.Id));

            ProductAdded.Should().BeNull();
        }
    }
}
