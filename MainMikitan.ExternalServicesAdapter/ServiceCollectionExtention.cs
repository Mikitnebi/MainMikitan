using MainMikitan.ExternalServicesAdapter.Email;
using Microsoft.Extensions.DependencyInjection;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.ExternalServicesAdapter
{
    public static class ServiceCollectionExtention
    {
        public static void AddMainMikitanExternalService(this IServiceCollection services)
        {
            //S3Adapter
            services.AddScoped<IS3Adapter, S3Adapter>();
            //emailsender
            services.AddScoped<IEmailSenderService, EmailSenderService>();
        }
    }
}
