using System;
using System.Linq.Expressions;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using OrderService.Presentation;
using OrderService.Application.Orders;
using OrderService.Domain.Models;

namespace OrderService.Presentation.Tests
{
    public class AppTests
    {
        [Fact]
        public void DisplayOrderSummary_LogsOrderSummary()
        {
            // Arrange
            var orderServiceMock = new Mock<IOrderService>();
            var loggerMock = new Mock<ILogger<App>>();
            var app = new App(orderServiceMock.Object, loggerMock.Object);
            var order = new Order { CustomerName = "Alice", ProductName = "Book", Quantity = 2, Price = 10.0 };
            double total = 20.0;

            // Act
            var method = typeof(App).GetMethod("DisplayOrderSummary", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(app, new object[] { order, total });

            // Assert
            loggerMock.Verify(l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Order Summary")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}
