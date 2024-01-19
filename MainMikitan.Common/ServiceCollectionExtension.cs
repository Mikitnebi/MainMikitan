using MainMikitan.InternalServiceAdapter.Auth;
using MainMikitan.InternalServiceAdapter.Hasher;
using Microsoft.Extensions.DependencyInjection;

namespace MainMikitan.InternalServicesAdapter
{
    public static class ServiceCollectionExtension
    {
        public static void AddMainMikitanInternalService(this IServiceCollection services)
        {
            //password
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //auth
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
