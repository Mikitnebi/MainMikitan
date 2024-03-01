using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.InternalServiceAdapter.Auth;
using MediatR;
using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Requests.RestaurantRequests.Auth;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Templates;
using Microsoft.AspNetCore.Identity;

namespace MainMikitan.Application.Features.Restaurant.Login;

public class StaffLoginCommand(StaffLoginRequest request) : ICommand<AuthTokenResponseModel>
{
    public readonly StaffLoginRequest Request = request;
}

public class StaffLoginCommandCommandHandler(
    IRestaurantStaffQueryRepository restaurantStaffQueryRepository,
    IPermissionService permissionService,
    IPasswordHasher passwordHasher,
    IAuthService authService)
    : 
        ResponseMaker<AuthTokenResponseModel>, 
        ICommandHandler<StaffLoginCommand, AuthTokenResponseModel>
{

    public async Task<ResponseModel<AuthTokenResponseModel>> Handle(StaffLoginCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var hashPassword = passwordHasher.Hash(command.Request.Password);
            var hashUserName = passwordHasher.Hash(command.Request.Username);
            var staff = await restaurantStaffQueryRepository.GetActive(hashUserName, hashPassword, cancellationToken);
            if (staff is null)
                return Fail(ErrorResponseType.Staff.IncorrectEmailOrPassword);
            return staff.IsManager
                ? Success(authService.StaffAuth(new StaffAuthModel
                {
                    StaffId = staff!.Id,
                    RestaurantId = staff!.RestaurantId,
                    IsManager = staff!.IsManager,
                }))
                : Success(authService.StaffAuth(new StaffAuthModel
                {
                    StaffId = staff!.Id,
                    RestaurantId = staff!.RestaurantId,
                    IsManager = staff!.IsManager,
                    Permissions = await permissionService.GetByStaffId(staff.Id, cancellationToken)
                }));
        }
        catch (Exception ex)
        {
            return Unexpected(ex);
        }
    }
}