using MainMikitan.ExternalServicesAdapter.Email;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
