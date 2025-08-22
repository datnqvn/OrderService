using LegacyOrderService.Models;

namespace LegacyOrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public OrderRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public void Save(Order order)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Orders (CustomerName, ProductName, Quantity, Price)
                        VALUES (@CustomerName, @ProductName, @Quantity, @Price)";
                    var param = command.CreateParameter();
                    param.ParameterName = "@CustomerName";
                    param.Value = order.CustomerName;
                    command.Parameters.Add(param);

                    param = command.CreateParameter();
                    param.ParameterName = "@ProductName";
                    param.Value = order.ProductName;
                    command.Parameters.Add(param);

                    param = command.CreateParameter();
                    param.ParameterName = "@Quantity";
                    param.Value = order.Quantity;
                    command.Parameters.Add(param);

                    param = command.CreateParameter();
                    param.ParameterName = "@Price";
                    param.Value = order.Price;
                    command.Parameters.Add(param);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void SeedBadData()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Orders (CustomerName, ProductName, Quantity, Price) VALUES ('John', 'Widget', 9999, 9.99)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
