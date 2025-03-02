namespace Net9HybridCacheDemo.Models;

public class APIResponse<T>
{
    public T Data { get; set; } = default!;
    public double ResponseTime { get; set; }
}