using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.InternalServiceAdapter.Auth;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.BC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.InternalServiceAdapterService
{
    public static class ServiceCollectionExtention
    {
        public static void AddMainMikitanInternalService(this IServiceCollection services)
        {
            //passwordhash
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //auth
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
