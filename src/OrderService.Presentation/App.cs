
using System;
using System.Collections.Generic;
using OrderService.Domain.Models;
using Microsoft.Extensions.Logging;
using OrderService.Application.Orders;

namespace OrderService.Presentation
{
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
                    _logger.LogWarning($"Product '{product}' not found. Please try again.");
                }
            }

            int quantity = PromptInt("Enter quantity:", "Quantity must be a positive integer.");

            return new Order
            {
                CustomerName = name,
                ProductName = product,
                Quantity = quantity,
                Price = price
            };
        }

        private void DisplayOrderSummary(Order order, double total)
        {
            _logger.LogInformation("\nOrder Summary:");
            _logger.LogInformation("Customer: {CustomerName}", order.CustomerName);
            _logger.LogInformation("Product: {ProductName}", order.ProductName);
            _logger.LogInformation("Quantity: {Quantity}", order.Quantity);
            _logger.LogInformation("Price: {Price}", order.Price.ToString("C"));
            _logger.LogInformation("Total: {Total}", total.ToString("C"));
        }

        private string PromptNonEmptyString(string prompt, string errorMsg)
        {
            while (true)
            {
                Console.Write(prompt + " ");
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                _logger.LogWarning(errorMsg);
            }
        }

        private int PromptInt(string prompt, string errorMsg)
        {
            while (true)
            {
                Console.Write(prompt + " ");
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                    return value;
                _logger.LogWarning(errorMsg);
            }
        }
    }
}
