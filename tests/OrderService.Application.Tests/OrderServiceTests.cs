using Xunit;
using Moq;
using OrderService.Domain.Models;
using OrderService.Domain.Interfaces;

using OrdersServiceImpl = OrderService.Application.Orders.OrderService;

namespace OrderService.Application.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void CalculateTotal_ReturnsCorrectValue()
        {
            // Arrange
            var order = new Order { Quantity = 2, Price = 10.5, CustomerName = "Test", ProductName = "Widget" };
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var service = new OrdersServiceImpl(mockOrderRepo.Object, mockProductRepo.Object);

            // Act
            var total = service.CalculateTotal(order);

            // Assert
            Assert.Equal(21.0, total);
        }

        [Fact]
        public void GetProductPrice_ReturnsExpectedPrice()
        {
            // Arrange
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(x => x.GetPrice("Widget")).Returns(12.99);
            var service = new OrdersServiceImpl(mockOrderRepo.Object, mockProductRepo.Object);

            // Act
            var price = service.GetProductPrice("Widget");

            // Assert
            Assert.Equal(12.99, price);
        }
    }
}
