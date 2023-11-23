using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer.Auth;
using MainMikitan.Domain.Requests.RestaurantRequests.Auth;

namespace MainMikitan.InternalServiceAdapter.Auth;

public interface IAuthService 
{
    ResponseModel<AuthTokenResponseModel> CustomerAuth(CustomerAuthRequestModel customerAuthModel);
    ResponseModel<AuthTokenResponseModel> RestaurantAuth(RestaurantAuthRequestModel customerAuthModel);
}
