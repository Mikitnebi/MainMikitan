namespace MainMikitan.Domain.Requests.TableRequests;

public record AddTableRequest(
    int MaxPlace,
    int MinPlace,
    int TableNumber,
    int TableType,
    decimal XCoordinate,
    decimal YCoordinate,
    List<int> EnvironmentId
);