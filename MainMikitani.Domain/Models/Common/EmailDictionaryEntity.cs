namespace MainMikitan.Domain.Models.Common;

public class EmailDictionaryEntity
{
    public int Id { get; set; }
    public string Body { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public int ReplacementQuantity { get; set; }
}
