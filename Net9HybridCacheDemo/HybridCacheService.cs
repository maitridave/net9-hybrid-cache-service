using System.Diagnostics;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Net9HybridCacheDemo.Models;
using Net9HybridCacheDemo.Repositories;

namespace Net9HybridCacheDemo;

public class HybridCacheService :  IHybridCacheService
{
    private readonly HybridCache _cache;
    private readonly IMemoryCache _memoryCache; // Access memory cache layer, this is added to cross check that the key exists in memory cache or not
    private readonly IDistributedCache _distributedCache; //Access distributed cache layer, this is added to cross check that the key exists in distributed cache or not
    private readonly ILocationsRepository _locationsRepository;

    
    public HybridCacheService(HybridCache cache, IMemoryCache memoryCache, IDistributedCache distributedCache,
        ILocationsRepository locationsRepository)
    {
        _cache = cache;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
        _locationsRepository = locationsRepository;
    }
    
    public bool CheckInMemoryCache(string key)
    {
        return _memoryCache.TryGetValue(key, out _); // Return true if the key exists
    }
    
    public bool CheckRedisCache(string key)
    {
        var value = _distributedCache.GetString(key);
        return value != null; // If value is not null, the key exists
    }
    
    public async Task<bool> RemoveLocationsAsync(CancellationToken ct = default)
    {
        string cacheKey = "LocationsMasterData";
        await _cache.RemoveAsync(cacheKey, ct);
        return true;
    }
    
    public async Task<List<DenormalizedZipCode>> GetLocationsAsync(CancellationToken ct = default)
    {
        string cacheKey = "LocationsMasterData";
        
        //Use HybridCache to fetch data from either L1 or L2 cache
        var data = await _cache.GetOrCreateAsync(cacheKey,
            async token => await _locationsRepository.GetAllAsync(),
            new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(30), //shared expiration for Memory and Redis
                LocalCacheExpiration = TimeSpan.FromMinutes(30) //Memory cache expiration
            },
            null,
            ct
        );

        return data;
    }
}


public interface IHybridCacheService
{
    
}