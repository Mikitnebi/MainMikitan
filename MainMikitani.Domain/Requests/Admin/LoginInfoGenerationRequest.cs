using MainMikitan.Domain.Requests.RestaurantRequests;

namespace MainMikitan.Domain.Requests.Admin;


public record LoginInfoGenerationRequest(RestaurantRegistrationRequest Restaurant,RestaurantStaffRegistrationRequest Manager);