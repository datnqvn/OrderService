using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Orders;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Factories;
using OrderService.Infrastructure.Repositories;
using Serilog;

namespace OrderService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var host = Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((ctx, services) =>
                {
                    var connectionString = ctx.Configuration.GetConnectionString("Default");
                    services.AddSingleton<IDbConnectionFactory>(new SqliteConnectionFactory(connectionString));
                    services.AddScoped<IOrderRepository, OrderRepository>();
                    var productDataFile = ctx.Configuration["ProductDataFile"] ?? "src/OrderService.Presentation/products.json";
                    services.AddScoped<IProductRepository>(sp => new ProductRepository(productDataFile));
                    services.AddScoped<IOrderService, Application.Orders.OrderService>();
                    services.AddScoped<App>();
                })
                .Build();

            host.Services.GetRequiredService<App>().Run(args);
        }
    }
}
