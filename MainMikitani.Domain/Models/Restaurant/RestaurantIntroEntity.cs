namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantIntroEntity
{
    public int Id { get; set; }
    public int StatusId { get; set; }
    public string EmailAddress { get; set; } = null!;
    public bool EmailConfirmation { get; set; }
    public int RestaurantOtpVerificationId { get; set; }
    public string BusinessNameGeo { get; set; } = null!;
    public string BusinessNameEng { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int RegionId { get; set; }
    public DateTime CreatedAt { get; set; }     
    public int ParentId { get; set; }
    public int ParentConfirmation { get; set; }
}
