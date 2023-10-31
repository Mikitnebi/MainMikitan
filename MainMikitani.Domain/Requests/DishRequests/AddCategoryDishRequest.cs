namespace MainMikitan.Domain.Requests;

public record AddCategoryDishRequest
{
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
    public DateTime CreateAt { get; set; } = DateTime.Now;
}