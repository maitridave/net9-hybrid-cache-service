using Net9HybridCacheDemo.Models;

namespace Net9HybridCacheDemo.Repositories;

public interface ILocationsRepository
{
    Task<List<DenormalizedZipCode>> GetAllAsync();
}