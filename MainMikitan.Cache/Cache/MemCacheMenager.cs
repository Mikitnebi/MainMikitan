using Microsoft.Extensions.Caching.Memory;
using NPOI.SS.Formula.Functions;

namespace MainMikitan.Cache.Cache;

public class MemCacheManager : IMemCacheManager
{
    private readonly IMemoryCache _memoryCache;
    public MemCacheManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public bool Set(string key, object value)
    {
        if (Get<object>(key) != null) return false;
        _memoryCache.Set(key, value, DateTimeOffset.Now.AddDays(1));
        return true;
    }
    public T? Get<T>(string key)
    {
        _memoryCache.TryGetValue(key, out var cacheValue);
        if (cacheValue == null) return default(T);
        return (T)cacheValue;
    }
    public bool Remove (string key)
    {
        if (Get<object>(key) == null) return false;
        _memoryCache.Remove(key);
        return true;
    }
    public void Clear()
    {
        _memoryCache.Dispose();
    }
}
