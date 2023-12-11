using MainMikitan.Cache.Cache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;

namespace MainMikitan.Cache
{
    public static class ServiceCollectionExtension
    {
        public static void AddMainMikitanCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IMemCacheManager, MemCacheManager>();
        }
    }
}
