using OrderService.Domain.Models;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        void Save(Order order);
        void SeedBadData();
    }
}
