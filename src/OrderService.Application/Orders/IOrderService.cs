using OrderService.Domain.Models;

namespace OrderService.Application.Orders
{
    public interface IOrderService
    {
        double GetProductPrice(string productName);
        void SaveOrder(Order order);
        double CalculateTotal(Order order);
    }
}
