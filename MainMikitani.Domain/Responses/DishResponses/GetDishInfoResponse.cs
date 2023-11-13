using Microsoft.AspNetCore.Http;

namespace MainMikitan.Domain.Responses.DishResponses;

public class GetDishInfoResponse
{
    public int DishId { get; set; }
    public string IngredientsGeo { get; set; } = null!;
    public string IngredientsEng { get; set; } = null!;
    public string DescriptionGeo { get; set; } = null!;
    public string DescriptionEng { get; set; } = null!;
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
    public bool IsActive { get; set; }
    public string CategoryNameGeo { get; set; } = null!;
    public string CategoryNameEng { get; set; } = null!;
    public int CategoryId { get; set; }
    public string DishPicture { get; set; } = null!;
    public DateTime CreateAt { get; set; }
}