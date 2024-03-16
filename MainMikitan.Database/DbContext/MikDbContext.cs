using MainMikitan.Database.DbContext.Mapping;
using MainMikitan.Domain.Models.Comments;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Events;
using MainMikitan.Domain.Models.ListOfValue;
using MainMikitan.Domain.Models.Logger;
using MainMikitan.Domain.Models.Menu;
using MainMikitan.Domain.Models.Rating;
using MainMikitan.Domain.Models.Reservation;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.DbContext;

public class MikDbContext : IdentityDbContext
{
    public MikDbContext(DbContextOptions<MikDbContext> option) : base(option) { }

    public DbSet<CategoryDishEntity> CategoryDish { get; set; }
    public DbSet<CustomerInfoEntity> CustomerInfo { get; set; }
    public DbSet<CustomerInterestEntity> CustomerInterest { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<DictionaryEntity> Dictionary { get; set; }
    public DbSet<DishEntity> Dish { get; set; }
    public DbSet<DishInfoEntity> DishInfo { get; set; }
    public DbSet<EmailDictionaryEntity> EmailDictionary { get; set; }
    public DbSet<EmailLogEntity> EmailLog { get; set; }
    public DbSet<OtpLogIntroEntity> OtpLogIntro { get; set; }
    public DbSet<RestaurantEntity> Restaurant { get; set; }
    public DbSet<RestaurantEnvEntity> RestaurantEnvironmentInfo { get; set; }
    public DbSet<RestaurantInfoEntity> RestaurantInfo { get; set; }
    public DbSet<RestaurantIntroEntity> RestaurantIntro { get; set; } 
    public DbSet<RestaurantStaffEntity> RestaurantStaff { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<ReservationEntity> Reservations { get; set; }
    public DbSet<RestaurantBranchingCodeLogEntity> RestaurantBranchingCodeLogs { get; set; }
    public DbSet<SectorEntity> Sector { get; set; }
    public DbSet<TableEnvironmentEntity> TableEnvironment { get; set; }
    public DbSet<TableInfoEntity> TableInfo { get; set; }
    public DbSet<PermissionEntity> Permission { get; set; }
    public DbSet<EventEntity> Event { get; set; }
    public DbSet<EventDetailsEntity> EventDetails { get; set; }
    public DbSet<RestaurantCommentEntity> RestaurantComments { get; set; }
    public DbSet<RestaurantRatingEntity> RestaurantRating { get; set; }
    public DbSet<CustomerRatingEntity> CustomerRating { get; set; }
    public DbSet<RestaurantSubscriptionsEntity> RestaurantSubscriptions { get; set; }
    public DbSet<RestaurantSubscriptionAndPermissionMapEntity> RestaurantSubscriptionAndPermissionMap { get; set; }
    public DbSet<RestaurantSubscriptionTypeEntity> RestaurantSubscriptionType { get; set; }
    public DbSet<LoggerEntity> Logger { get; set; }
    public DbSet<ReservationRatingsEntity> ReservationRatings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryDishMap());
        modelBuilder.ApplyConfiguration(new CustomerInfoMap());
        modelBuilder.ApplyConfiguration(new CustomerInterestMap());
        modelBuilder.ApplyConfiguration(new CustomersMap());
        modelBuilder.ApplyConfiguration(new DictionaryMap());
        modelBuilder.ApplyConfiguration(new DishMap());
        modelBuilder.ApplyConfiguration(new DishInfoMap());
        modelBuilder.ApplyConfiguration(new EmailDictionaryMap());
        modelBuilder.ApplyConfiguration(new EmailLogMap());
        modelBuilder.ApplyConfiguration(new OtpLogIntroMap());
        modelBuilder.ApplyConfiguration(new RestaurantMap());
        modelBuilder.ApplyConfiguration(new RestaurantEnvironmentInfoMap());
        modelBuilder.ApplyConfiguration(new RestaurantInfoMap());
        modelBuilder.ApplyConfiguration(new RestaurantIntroMap());
        modelBuilder.ApplyConfiguration(new RestaurantStaffMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new TableEnvironmentMap());
        modelBuilder.ApplyConfiguration(new TableInfoMap());
        modelBuilder.ApplyConfiguration(new EventMap());
        modelBuilder.ApplyConfiguration(new EventDetailsMap());
        modelBuilder.ApplyConfiguration(new ReservationMap());
        modelBuilder.ApplyConfiguration(new RestaurantCommentMap());
        modelBuilder.ApplyConfiguration(new RestaurantRatingMap());
        modelBuilder.ApplyConfiguration(new CustomerRatingMap());
        modelBuilder.ApplyConfiguration(new ReservationRatingsMap());
        
        base.OnModelCreating(modelBuilder);
    }
}
