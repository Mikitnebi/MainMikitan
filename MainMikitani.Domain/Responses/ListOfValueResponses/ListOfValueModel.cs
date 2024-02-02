using MainMikitan.Domain.Models.ListOfValue;

namespace MainMikitan.Domain.Responses.ListOfValueResponses;

public record ListOfValueModel
{
    public SectorEntity Sector { get; set; } = null!;
    public List<DictionaryModel> Dictionaries{ get; set; } = null!;
}