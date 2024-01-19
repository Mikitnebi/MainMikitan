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
    public int RestaurantId { get; set; }
    public bool IsCompany { get; set; }
    public decimal Value { get; set; }
    public int TableId  { get; set; }
    public long ReservedManuId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public string? Comment { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsCompleted => Date > DateTime.Now && !IsCanceled;
}