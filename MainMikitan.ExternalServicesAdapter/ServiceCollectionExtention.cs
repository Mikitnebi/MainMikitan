using MainMikitan.ExternalServicesAdapter.Email;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.ExternalServicesAdapter
{
    public static class ServiceCollectionExtention
    {
        public static void AddMainMikitanExternalService(this IServiceCollection services)
        {
            //emailsender
            services.AddScoped<IEmailSenderService, EmailSenderService>();
        }
    }
}
