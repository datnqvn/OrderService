using System.Collections.Generic;
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
            var repo = new ProductRepository();

            // Act
            var price = repo.GetPrice("Widget");

            // Assert
            Assert.Equal(12.99, price);
        }

        [Fact]
        public void GetPrice_ThrowsKeyNotFoundException_ForUnknownProduct()
        {
            // Arrange
            var repo = new ProductRepository();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => repo.GetPrice("Unknown"));
        }
    }
}
