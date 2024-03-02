using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Requests.TableRequests;

public record AddTableRequest
{
    [Range(1, 20)]
    public int MaxPlace { get; init; }
    [Range(1, 20)]
    public int MinPlace { get; init; }
    [Range(1, 100)]
    public int TableNumber { get; init; }
    public int TableType { get; init; }
    [Range(1, 1000)]
    public decimal XCoordinate { get; init; }
    [Range(1, 1000)]
    public decimal YCoordinate { get; init; }

    [Length(0, 3)] public List<int> EnvironmentIds { get; init; } = null!;
}