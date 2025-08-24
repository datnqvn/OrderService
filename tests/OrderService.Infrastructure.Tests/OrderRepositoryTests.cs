using System.Data;
using Xunit;
using Moq;
using OrderService.Infrastructure.Repositories;
using OrderService.Domain.Models;

namespace OrderService.Infrastructure.Tests
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void Save_CallsCreateConnectionAndExecutesCommand()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();
            var mockCommand = new Mock<IDbCommand>();
            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockConnection.SetupProperty(c => c.ConnectionString);
            mockCommand.SetupAllProperties();
            mockCommand.Setup(c => c.CreateParameter()).Returns(() => new Mock<IDbDataParameter>().Object);
            mockCommand.SetupGet(c => c.Parameters).Returns(new Mock<IDataParameterCollection>().Object);

            var mockFactory = new Mock<OrderService.Infrastructure.Factories.IDbConnectionFactory>();
            mockFactory.Setup(f => f.CreateConnection()).Returns(mockConnection.Object);

            var repo = new OrderRepository(mockFactory.Object);
            var order = new Order { CustomerName = "Test", ProductName = "Widget", Quantity = 1, Price = 9.99 };

            // Act
            repo.Save(order);

            // Assert
            mockFactory.Verify(f => f.CreateConnection(), Times.Once);
            mockConnection.Verify(c => c.Open(), Times.Once);
            mockCommand.VerifySet(c => c.CommandText = It.IsAny<string>(), Times.Once);
            mockCommand.Verify(c => c.ExecuteNonQuery(), Times.Once);
        }
    }
}
