using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace OrderService.Infrastructure.Factories
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class SqliteConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqliteConnectionFactory(string? connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}
