using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Menu;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.DbContext;

public class MikDbContext : IdentityDbContext
{
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
    public DbSet<RestaurantEnvironmentInfoEntity> RestaurantEnvironmentInfo { get; set; }
    public DbSet<RestaurantInfoEntity> RestaurantInfo { get; set; }
    public DbSet<RestaurantIntroEntity> RestaurantIntro { get; set; }
    // public DbSet<RestaurantStaffEntity> RestaurantStaff { get; set; }
    
    public MikDbContext(DbContextOptions<MikDbContext> option) : base(option)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
