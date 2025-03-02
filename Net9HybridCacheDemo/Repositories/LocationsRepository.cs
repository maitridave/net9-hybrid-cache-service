using Microsoft.EntityFrameworkCore;
using Net9HybridCacheDemo.Data;
using Net9HybridCacheDemo.Models;

namespace Net9HybridCacheDemo.Repositories;

public class LocationsRepository : ILocationsRepository
{
    private readonly DatabaseContext _context;

    public LocationsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<DenormalizedZipCode>> GetAllAsync()
    {
        return await _context.DenormalizedZipCode.ToListAsync();
    }
}