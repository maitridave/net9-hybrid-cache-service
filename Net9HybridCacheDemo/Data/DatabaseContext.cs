using Microsoft.EntityFrameworkCore;
using Net9HybridCacheDemo.Models;

namespace Net9HybridCacheDemo.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<DenormalizedZipCode> DenormalizedZipCode { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}