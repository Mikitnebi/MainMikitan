using MainMikitan.Database.Features.Common.Email.Command;
using MainMikitan.Database.Features.Common.Query;
using MainMikitan.Database.Features.Customer.Command;
using MainMikitan.Database.Features.Customer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MainMikitan.Database.Features.Common.Otp.Command;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Common.Otp.Query;
using MainMikitan.Database.Features.Common.Email.Interfaces;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Database.Features.Restaurant.Command;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Database.Features.Restaurant.Query;
using MainMikitan.Database.Features.Category.Query;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Query;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Database.Features.Common.Multifunctional.Query;
using MainMikitan.Database.Features.Common.Multifunctional.Repository;
using MainMikitan.Database.Features.Dish.Command;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Database.Features.Restaurant.Interface;

namespace MainMikitan.Database
{
    public static class ServiceCollectionExtension
    {
        public static void AddMainMikitanDatabase(this IServiceCollection services)
        {
            //customer
            services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
            services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
            services.AddScoped<ICustomerInterestRepository, CustomerInterestRepository>();
            services.AddScoped<ICustomerInfoRepository, CustomerInfoRepository>();

            //email
            services.AddScoped<IEmailSenderQueryRepository, EmailSenderQueryRepository>();
            services.AddScoped<IEmailLogCommandRepository, EmailLogCommandRepository>();

            //otp
            services.AddScoped<IOtpLogCommandRepository, OtpLogCommandRepository>();
            services.AddScoped<IOtpLogQueryRepository, OtpLogQueryRepository>();
            
            //restaurant
            services.AddScoped<IRestaurantIntroCommandRepository, RestaurantIntroCommandRepository>();
            services.AddScoped<IRestaurantIntroQueryRepository, RestaurantIntroQueryRepository>();
            services.AddScoped<IRestaurantFinalCommandRepository, RestaurantFinalCommandRepository>();
            services.AddScoped<IRestaurantCommandRepository, RestaurantCommandRepository>();

            services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
            
            //multifunctional Repository
            services.AddScoped<IMultifunctionalQuery, MultifunctionalQuery>();
            services.AddScoped<IMultifunctionalRepository, MultifunctionalRepository>();
            
            //Dish Repository
            services.AddScoped<IDishCommandRepository, DishCommandRepository>();

        }
    }
}
