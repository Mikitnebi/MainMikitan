namespace MainMikitan.Domain.Requests;

public record AddDishInfoRequest
{
    public int DishId { get; set; }
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
    public string IngredientsGeo { get; set; } = null!;
    public string IngredientsEng { get; set; } = null!;
    public string? DescriptionGeo { get; set; }
    public string? DescriptionEng { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
}