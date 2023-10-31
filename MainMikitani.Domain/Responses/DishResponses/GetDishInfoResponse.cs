namespace MainMikitan.Domain.Responses.DishResponses;

public class GetDishInfoResponse
{
    public string IngredientsGeo { get; set; } = null!;
    public string IngredientsEng { get; set; } = null!;
    public string DescriptionGeo { get; set; } = null!;
    public string DescriptionEng { get; set; } = null!;
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
    public DateTime CreateAt { get; set; }
}