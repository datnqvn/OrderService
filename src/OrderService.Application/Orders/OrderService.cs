using OrderService.Domain.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;

        public OrderService(IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public double GetProductPrice(string productName)
        {
            return _productRepo.GetPrice(productName);
        }

        public void SaveOrder(Order order)
        {
            _orderRepo.Save(order);
        }

        public double CalculateTotal(Order order)
        {
            return order.Quantity * order.Price;
        }
    }
}
