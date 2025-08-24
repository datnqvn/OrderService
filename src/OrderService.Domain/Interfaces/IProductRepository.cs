namespace OrderService.Domain.Interfaces
{
    public interface IProductRepository
    {
        double GetPrice(string productName);
    }
}
