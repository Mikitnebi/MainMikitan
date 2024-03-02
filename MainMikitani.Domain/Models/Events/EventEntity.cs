namespace MainMikitan.Domain.Models.Events;

public class EventEntity
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreationDate { get; set; }
}