namespace MainMikitan.Domain.Responses.ListOfValueResponses;

public record DictionaryModel
{
    public int Id { get; set; }
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
}