using LegacyOrderService.Models;

namespace LegacyOrderService.Application
{
    public interface IOrderService
    {
        double GetProductPrice(string productName);
        void SaveOrder(Order order);
        double CalculateTotal(Order order);
    }
}
