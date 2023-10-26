namespace MainMikitan.Domain.Models.Menu;

public class CategoryDishEntity
{
    public int Id { get; set; }
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}