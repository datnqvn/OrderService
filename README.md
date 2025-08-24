# OrderService

## Quick Start Guide

### Prerequisites
- .NET 8.0 SDK or later ([Download here](https://dotnet.microsoft.com/download))
- Git (for cloning the repository)

### Configuration
1. **Product Data:**
	- Edit `src/OrderService.Presentation/products.json` to add or update product prices.
2. **App Settings:**
	- Edit `src/OrderService.Presentation/appsettings.json` to configure the path to `products.json` (default is `src/OrderService.Presentation/products.json`).
3. **Database:**
	- The solution uses a local SQLite database file `orders.db` (copied automatically on build).

### Build
From the solution root directory:
```bash
dotnet build OrderService.sln --configuration Debug
```
or for Release:
```bash
dotnet build OrderService.sln --configuration Release
```

### Run
From the solution root directory:
```bash
dotnet run --project src/OrderService.Presentation/OrderService.Presentation.csproj
```

### Test
From the solution root directory:
```bash
dotnet test OrderService.sln --no-build --verbosity normal
```

### Notes
- All configuration files and data files are copied to the output directory on build.
- For custom product data, update `products.json` and ensure the path is correct in `appsettings.json`.
- For troubleshooting, check logs in `app.log` (created in the run directory).