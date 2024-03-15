using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Models.Reservation;

public record ReservationEntity
{
    [Required]
    public long Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int GuestAmount { get; set; }
    [Required]
    public int RestaurantId { get; set; }
    public bool IsCompany { get; set; }
    public decimal Value { get; set; }
    public int TableId  { get; set; }
    public long ReservedManuId { get; set; }
    public DateTime DateAt { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public string? Comment { get; set; }
    public bool GuestArrived { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsCompleted
    {
        get => IsCompleted;
        set => IsCompleted = DateAt > DateTime.Now && !IsCanceled;
    }
}