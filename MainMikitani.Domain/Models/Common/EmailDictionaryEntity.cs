namespace MainMikitan.Domain.Models.Common;

public class EmailDictionaryEntity
{
    public int Id { get; set; }
    public string Body { get; set; }
    public string Subject { get; set; }
    public int ReplacementQuantity { get; set; }
}
