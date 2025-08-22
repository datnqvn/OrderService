using System.Data;

namespace LegacyOrderService.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
