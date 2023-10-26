namespace MainMikitan.Domain.Models.Restaurant
{
    public class RestaurantEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get;set; }
        public DateTime? UpdatedAt { get;set;}
    }
}
