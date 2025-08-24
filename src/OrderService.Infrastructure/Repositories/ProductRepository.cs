using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using OrderService.Domain.Interfaces;

namespace OrderService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<string, double> _productPrices;

        public ProductRepository(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException($"Product data file not found: {jsonFilePath}");

            var json = File.ReadAllText(jsonFilePath);
            _productPrices = JsonSerializer.Deserialize<Dictionary<string, double>>(json)
                ?? new Dictionary<string, double>();
        }

        public double GetPrice(string productName)
        {
            // Simulate an expensive lookup
            Thread.Sleep(500);

            if (_productPrices.TryGetValue(productName, out var price))
                return price;

            throw new KeyNotFoundException($"Product '{productName}' not found");
        }
    }
}
