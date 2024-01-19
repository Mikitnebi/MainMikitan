using MainMikitan.ExternalServicesAdapter.Email;
using Microsoft.Extensions.DependencyInjection;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.ExternalServicesAdapter
{
    public static class ServiceCollectionExtension
    {
        public static void AddMainMikitanExternalService(this IServiceCollection services)
        {
            //S3Adapter
            services.AddScoped<IS3Adapter, S3Adapter>();
            //email sender
            services.AddScoped<IEmailSenderService, EmailSenderService>();
        }
    }
}
