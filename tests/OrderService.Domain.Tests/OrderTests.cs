using Xunit;
using OrderService.Domain.Models;

namespace OrderService.Domain.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Order_Properties_AreSetCorrectly()
        {
            // Arrange
            var order = new Order
            {
                CustomerName = "Alice",
                ProductName = "Widget",
                Quantity = 3,
                Price = 9.99
            };

            // Assert
            Assert.Equal("Alice", order.CustomerName);
            Assert.Equal("Widget", order.ProductName);
            Assert.Equal(3, order.Quantity);
            Assert.Equal(9.99, order.Price);
        }
    }
}
