using MainMikitan.Cache.Cache;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Cache
{
    public static class ServiceCollectionExtension
    {
        public static void AddMainMikitanDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IMemCacheManager, MemCacheMeneger>();
        }
    }
}
