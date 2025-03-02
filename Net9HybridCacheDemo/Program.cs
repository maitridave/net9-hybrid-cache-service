using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Net9HybridCacheDemo;
using Net9HybridCacheDemo.Data;
using Net9HybridCacheDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database configuration
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add MemoryCache and DistributedCache
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

// Configure and register HybridCache
#pragma warning disable EXTEXP0018
builder.Services.AddHybridCache(options =>
{
    options.MaximumPayloadBytes = 100 * 1024 * 1024; //104 mb
    options.MaximumKeyLength = 512;
    
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(30),
        LocalCacheExpiration = TimeSpan.FromMinutes(30)
    };
});
#pragma warning restore EXTEXP0018

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisCache");
});

// Optional: If you have custom services requiring access to IConfiguration
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
//Repository
builder.Services.AddScoped<ILocationsRepository, LocationsRepository>();
// Register your HybridCacheService that depends on HybridCache
builder.Services.AddTransient<HybridCacheService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure middleware
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseDeveloperExceptionPage();
app.UseCors("AllowAll");

app.Run();