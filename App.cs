
using LegacyOrderService.Application;
using LegacyOrderService.Models;
using Microsoft.Extensions.Logging;


public sealed class App
{
    private readonly IOrderService _orderService;
    private readonly ILogger<App> _logger;

    public App(IOrderService orderService, ILogger<App> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public void Run(string[] args)
    {
        try
        {
            _logger.LogInformation("Welcome to Order Processor!");
            var order = ProcessOrderInput();
            double total = _orderService.CalculateTotal(order);
            DisplayOrderSummary(order, total);
            _logger.LogInformation("Saving order to database...");
            _orderService.SaveOrder(order);
            _logger.LogInformation("Done.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
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
                _logger.LogWarning("The product you entered does not exist. Please check the product name and try again.");
            }
        }

        int qty = PromptPositiveInt("Enter quantity:", "Quantity must be a positive integer. Please try again.");

        _logger.LogInformation("Processing order...");

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
        _logger.LogInformation("Order complete!");
        _logger.LogInformation("Customer: {CustomerName}", order.CustomerName);
        _logger.LogInformation("Product: {ProductName}", order.ProductName);
        _logger.LogInformation("Quantity: {Quantity}", order.Quantity);
        _logger.LogInformation("Total: ${Total}", total);
    }

    private string PromptNonEmptyString(string prompt, string errorMessage)
    {
        while (true)
        {
            _logger.LogInformation(prompt);
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input;
            _logger.LogWarning(errorMessage);
        }
    }

    private int PromptPositiveInt(string prompt, string errorMessage)
    {
        while (true)
        {
            _logger.LogInformation(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out int value) && value > 0)
                return value;
            _logger.LogWarning(errorMessage);
        }
    }
}
