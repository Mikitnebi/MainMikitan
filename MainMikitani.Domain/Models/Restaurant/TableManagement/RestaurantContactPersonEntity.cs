namespace MainMikitan.Domain.Models.Restaurant.TableManagement;

public record RestaurantContactPersonEntity
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int StaffId { get; set; }
    public DateTime UpdatedAt { get; set; }
}