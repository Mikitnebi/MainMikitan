namespace MainMikitan.Domain.Requests.Customer.Feature.Reservation;

public record ReservationRequest(
    int RestaurantId, 
    int GuestNumber, 
    int? AverageHour, 
    List<int> EnvironmentTypeId, 
    DateTime DateAt,
    int PurposeTypeId,
    string Comment,
    bool IsAnyBaby,
    bool IsAnyAnimal);