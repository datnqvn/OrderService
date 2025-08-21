namespace LegacyOrderService.Models
{
    public class Order
    {
        public required string CustomerName { get; set; }
        public required string ProductName { get; set; }
        public required int Quantity { get; set; }
        public required double Price { get; set; }
    }
}
