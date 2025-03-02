# dotnet-9-hybrid-cache-service
This repository contains source code for the .Net9 hybrid cache using in-memory and Redis caches.

### Built With

* .NET SDK
* SQL Server (or any other database you use)
* Redis

## Configuration

### appsettings.json

Configure your connection strings and logging settings in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "RedisCache": "RedisConnectionString",
    "DatabaseConnectionString": "DatabaseConnectionString"
  },
  "AllowedHosts": "*"
}

## Getting Started

1. Clone the repository:  
    git clone https://github.com/yourusername/Net9HybridCacheDemo.git
    cd Net9HybridCacheDemo
2. Install the required packages:  
    dotnet restore
3. Update the connection strings in appsettings.json to match your environment.  
4. Run the application:  
    dotnet run
5. Open your browser and navigate to http://localhost:5091/swagger/index.html to view the Swagger UI.