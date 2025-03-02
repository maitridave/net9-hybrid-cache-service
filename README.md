# dotnet-9-hybrid-cache-service
This repository contains source code for the .Net9 hybrid cache using in-memory and Redis caches.

## Built With

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
```

## Getting Started

1. Clone the repository
2. Install the required packages:  
   ```sh
   dotnet restore
3. Update the connection strings in appsettings.json to match your environment.  
4. Run the application:
   ```sh 
   dotnet run
5. To view the Swagger UI, Open your browser and navigate to:
   ```sh
   http://localhost:5091/swagger/index.html.
