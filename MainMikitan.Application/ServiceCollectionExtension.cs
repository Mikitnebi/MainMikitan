using MainMikitan.Application.Features.Customer.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MainMikitan.Application.Services.CacheServices;
using MainMikitan.Application.Services.Otp;

namespace MainMikitan.Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddMainMikitanApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<ICustomerCacheService, CustomerCacheService>();
            services.AddScoped<IOtpCheckerService, OtpCheckerService>();
        }
    }
}
