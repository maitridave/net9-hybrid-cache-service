using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Net9HybridCacheDemo.Models;

namespace Net9HybridCacheDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HybridCacheController : ControllerBase
{
    private readonly HybridCacheService _hybridCacheService;

    public HybridCacheController(HybridCacheService hybridCacheService)
    {
        _hybridCacheService = hybridCacheService;
    }
    
    [HttpGet("getLocations")]
    public async Task<ActionResult<APIResponse<List<DenormalizedZipCode>>>> GetOrSetData()
    {
        var stopwatch = Stopwatch.StartNew();
        var items = await _hybridCacheService.GetLocationsAsync();
        stopwatch.Stop();

        if (items == null)
            return NotFound();

        return Ok(new APIResponse<List<DenormalizedZipCode>>()
        {
            Data = items.Take(5).ToList(),
            ResponseTime = stopwatch.ElapsedMilliseconds
        });
    }

    [HttpPost("removeLocations")]
    public async Task<IActionResult> InvalidateCache()
    {
        var items = await _hybridCacheService.RemoveLocationsAsync();
        return Ok();
    }
}