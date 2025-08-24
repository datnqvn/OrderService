using Xunit;
using OrderService.Infrastructure.Factories;

namespace OrderService.Infrastructure.Tests
{
    public class SqliteConnectionFactoryTests
    {
        [Fact]
        public void CreateConnection_ReturnsSqliteConnection()
        {
            // Arrange
            var factory = new SqliteConnectionFactory("Data Source=:memory:");

            // Act
            using var connection = factory.CreateConnection();

            // Assert
            Assert.NotNull(connection);
            Assert.Equal("Microsoft.Data.Sqlite.SqliteConnection", connection.GetType().FullName);
        }
    }
}
