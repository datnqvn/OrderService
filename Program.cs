using LegacyOrderService.Models;
using LegacyOrderService.Data;
using Microsoft.Extensions.DependencyInjection;
// Setup DI
var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();
serviceCollection.AddSingleton<IProductRepository, ProductRepository>();
var serviceProvider = serviceCollection.BuildServiceProvider();

var productRepo = serviceProvider.GetRequiredService<IProductRepository>();
var orderRepo = serviceProvider.GetRequiredService<IOrderRepository>();

try
{
    Console.WriteLine("Welcome to Order Processor!");
    Console.WriteLine("Enter customer name:");
    string? name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Customer name cannot be empty.");
        return;
    }

    Console.WriteLine("Enter product name:");
    string? product = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(product))
    {
        Console.WriteLine("Product name cannot be empty.");
        return;
    }
    double price = productRepo.GetPrice(product);

    Console.WriteLine("Enter quantity:");
    int qty = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Processing order...");

    Order order = new Order();
    order.CustomerName = name;
    order.ProductName = product;
    order.Quantity = qty;
    order.Price = price;

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
catch (KeyNotFoundException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("The product you entered does not exist. Please check the product name and try again.");
}
catch (Exception ex)
{
    Console.WriteLine("An unexpected error occurred: " + ex.Message);
    Console.WriteLine(ex.StackTrace);
}
