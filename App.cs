using LegacyOrderService.Application;
using LegacyOrderService.Models;

public sealed class App
{
    private readonly IOrderService _orderService;

    public App(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public void Run(string[] args)
    {
        try
        {
            Console.WriteLine("Welcome to Order Processor!");
            var order = ProcessOrderInput();
            double total = _orderService.CalculateTotal(order);
            DisplayOrderSummary(order, total);
            Console.WriteLine("Saving order to database...");
            _orderService.SaveOrder(order);
            Console.WriteLine("Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    private Order ProcessOrderInput()
    {
        string name = PromptNonEmptyString("Enter customer name:", "Customer name cannot be empty. Please try again.");

        string product;
        double price;
        while (true)
        {
            product = PromptNonEmptyString("Enter product name:", "Product name cannot be empty. Please try again.");
            try
            {
                price = _orderService.GetProductPrice(product);
                break;
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("The product you entered does not exist. Please check the product name and try again.");
            }
        }

        int qty = PromptPositiveInt("Enter quantity:", "Quantity must be a positive integer. Please try again.");

        Console.WriteLine("Processing order...");

        return new Order
        {
            CustomerName = name,
            ProductName = product,
            Quantity = qty,
            Price = price
        };
    }

    private void DisplayOrderSummary(Order order, double total)
    {
        Console.WriteLine("Order complete!");
        Console.WriteLine($"Customer: {order.CustomerName}");
        Console.WriteLine($"Product: {order.ProductName}");
        Console.WriteLine($"Quantity: {order.Quantity}");
        Console.WriteLine($"Total: ${total}");
    }

    private string PromptNonEmptyString(string prompt, string errorMessage)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input;
            Console.WriteLine(errorMessage);
        }
    }

    private int PromptPositiveInt(string prompt, string errorMessage)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out int value) && value > 0)
                return value;
            Console.WriteLine(errorMessage);
        }
    }
}
