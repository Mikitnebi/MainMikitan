using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.InternalServiceAdapter.Auth;
using MediatR;
using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Requests.RestaurantRequests.Auth;
using MainMikitan.Domain.Interfaces.Restaurant;

namespace MainMikitan.Application.Features.Restaurant.Login;

public class RestaurantLoginCommand : IRequest<ResponseModel<AuthTokenResponseModel>>
{
    public string _Username { get; set; }
    public string _Password { get; set; }
    public RestaurantLoginCommand(RestaurantLoginRequest request)
    {
        _Username = request.Username;
        _Password = request.Password;
    }
}

public class RestaurantLoginCommandHandler : IRequestHandler<RestaurantLoginCommand, ResponseModel<AuthTokenResponseModel>>
{
    private readonly IRestaurantIntroQueryRepository _restaurantIntroQueryRepository;
    private readonly IAuthService _authService;
    private readonly IPasswordHasher _passwordHasher;
    public RestaurantLoginCommandHandler
        (
        IRestaurantIntroQueryRepository restaurantIntroQueryRepository,
        IPasswordHasher passwordHasher,
        IAuthService authService)
    {
        _restaurantIntroQueryRepository = restaurantIntroQueryRepository;
        _passwordHasher = passwordHasher;
        _authService = authService;
    }
    public async Task<ResponseModel<AuthTokenResponseModel>> Handle(RestaurantLoginCommand command, CancellationToken cancellationToken)
    {
        var response = new ResponseModel<AuthTokenResponseModel>();
        var username = command._Username;
        var password = command._Password;
        var restaurant = await _restaurantIntroQueryRepository.GetByUsername(username);
        if (restaurant == null || !_passwordHasher.VerifyPassword(password, restaurant.PasswordHash))
        {
            response.ErrorType = ErrorType.NotCorrectEmailOrPassword;
            return response;
        }
        response = _authService.RestaurantAuth(new RestaurantAuthRequestModel
        {
            Username = username,
            Id = restaurant.Id
        });
        return response;
    }
}