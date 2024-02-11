using MainMikitan.Database.Features.Common.Email.Command;
using MainMikitan.Database.Features.Common.Query;
using MainMikitan.Database.Features.Customer.Command;
using MainMikitan.Database.Features.Customer;
using Microsoft.Extensions.DependencyInjection;
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
using MainMikitan.Database.Features.Customer.Query;
using MainMikitan.Database.Features.Dish.Command;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Database.Features.ListOfValue.Intefaces;
using MainMikitan.Database.Features.ListOfValue.Query;
using MainMikitan.Database.Features.Reservation.Command;
using MainMikitan.Database.Features.Reservation.Interfaces;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Database.Features.Table.Command;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Database.Features.Table.Query;
using MainMikitan.Domain.Models.Restaurant;

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
            services.AddScoped<ICustomerInterestQueryRepository, CustomerInterestQueryRepository>();

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
            services.AddScoped<IRestaurantEnvironmentRepository, RestaurantEnvironmentRepository>();

            services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
            
            //multifunctional Repository
            services.AddScoped<IMultifunctionalQuery, MultifunctionalQuery>();
            services.AddScoped<IMultifunctionalRepository, MultifunctionalRepository>();
            
            //Dish Repository
            services.AddScoped<IDishCommandRepository, DishCommandRepository>();
            
            //Reservation
            services.AddScoped<IReservationCommandRepository, ReservationCommandRepository>();
            
            //menu
            services.AddScoped<IGetMenuRepository, GetMenuRepository>();
            services.AddScoped<IRestaurantBranchingCodeLogRepository, RestaurantBranchingCodeLogRepository>();
            
            //ListOfValue
            services.AddScoped<IListOfValueQueryRepository, ListOfValueQueryRepository>();
            
            //Table
            services.AddScoped<ITableCommandRepository, TableCommandRepository>();
            services.AddScoped<ITableEnvironmentCommandRepository, TableEnvironmentCommandRepository>();
            services.AddScoped<ITableQueryRepository, TableQueryRepository>();
            
            //Staff
            services.AddScoped<IRestaurantStaffCommandRepository, RestaurantStaffCommandRepository>();
            services.AddScoped<IRestaurantStaffQueryRepository, RestaurantStaffQueryRepository>();
            services.AddScoped<IStaffPermissionQueryRepository, StaffPermissionQueryRepository>();
        }
    }
}
