using System.Runtime.InteropServices.JavaScript;

namespace MainMikitan.Domain.Models.Restaurant;

public record RestaurantBranchingCodeLogEntity
{
    public int Id { get; set; }
    public int ParentRestaurantId { get; set; }
    public int? ChildRestaurantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string Code { get; set; } = null!;
    public int? ValidateTime { get; set; }
    public int? NumberOfTrials {get; set; }
}