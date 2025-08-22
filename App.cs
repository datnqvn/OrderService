using LegacyOrderService.Data;
using LegacyOrderService.Models;

public sealed class App
{
    private readonly IOrderRepository orderRepo;
    private readonly IProductRepository productRepo;

    public App(IOrderRepository orderRepo, IProductRepository productRepo)
    {
        this.orderRepo = orderRepo;
        this.productRepo = productRepo;
    }

    public async Task RunAsync(string[] args, CancellationToken ct = default)
    {
        try
        {
            Console.WriteLine("Welcome to Order Processor!");
            string? name = null;
            while (true)
            {
                Console.WriteLine("Enter customer name:");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Customer name cannot be empty. Please try again.");
                }
                else
                {
                    break;
                }
            }

            string? product;
            double price;
            while (true)
            {
                Console.WriteLine("Enter product name:");
                product = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(product))
                {
                    Console.WriteLine("Product name cannot be empty. Please try again.");
                    continue;
                }
                try
                {
                    price = productRepo.GetPrice(product);
                    break;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("The product you entered does not exist. Please check the product name and try again.");
                }
            }

            int qty = 0;
            while (true)
            {
                Console.WriteLine("Enter quantity:");
                string? qtyInput = Console.ReadLine();
                if (!int.TryParse(qtyInput, out qty) || qty <= 0)
                {
                    Console.WriteLine("Quantity must be a positive integer. Please try again.");
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Processing order...");

            Order order = new Order
            {
                CustomerName = name,
                ProductName = product,
                Quantity = qty,
                Price = price
            };

            double total = order.Quantity * order.Price;

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + total);

            Console.WriteLine("Saving order to database...");
            orderRepo.Save(order);
            Console.WriteLine("Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }
}
