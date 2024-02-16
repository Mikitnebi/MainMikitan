using MainMikitan.Application.Features.Customer.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MainMikitan.Application.Services.AutoMapper;
using MainMikitan.Application.Services.CacheServices;
using MainMikitan.Application.Services.Filter;
using MainMikitan.Application.Services.Otp;
using MainMikitan.Application.Services.Permission;

namespace MainMikitan.Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddMainMikitanApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ICustomerCacheService, CustomerCacheService>();
            services.AddScoped<IOtpCheckerService, OtpCheckerService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<IMapperConfig, MapperConfig>();
        }
    }
}
