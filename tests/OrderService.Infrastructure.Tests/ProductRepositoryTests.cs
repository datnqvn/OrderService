using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void GetPrice_ReturnsCorrectPrice_ForKnownProduct()
        {
            // Arrange
            var filePath = "test_products.json";
            Assert.True(File.Exists(filePath), $"Test data file not found: {filePath}");
            var repo = new ProductRepository(filePath);

            // Act
            var price = repo.GetPrice("Widget");

            // Assert
            Assert.Equal(12.99, price);
        }

        [Fact]
        public void GetPrice_ThrowsKeyNotFoundException_ForUnknownProduct()
        {
            // Arrange
            var filePath = Path.Combine(AppContext.BaseDirectory, "test_products.json");
            Assert.True(File.Exists(filePath), $"Test data file not found: {filePath}");
            var repo = new ProductRepository(filePath);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => repo.GetPrice("Unknown"));
        }
    }
}
