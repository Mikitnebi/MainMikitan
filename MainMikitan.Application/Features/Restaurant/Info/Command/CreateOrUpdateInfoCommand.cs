using AutoMapper;
using MainMikitan.Application.Services.AutoMapper;
using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Templates;
using MediatR;

namespace MainMikitan.Application.Features.Restaurant.Info {
    public class CreateOrUpdateInfoCommand(RestaurantInfoRequest request, int userId, string userRole, int restaurantId, IEnumerable<int> permissionIds) : ICommand
        {
        public RestaurantInfoRequest RestaurantInfoRequest { get; set; } = request;
        public int UserId { get; set; } = userId;
        public string UserRole { get; set; } = userRole;
        public int RestaurantId { get; set; } = restaurantId;
        public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    }

    public class CreateOrUpdateInfoCommandHandler(
        IPermissionService permissionService,
        IRestaurantInfoCommandRepository restaurantInfoCommandRepository,
        IRestaurantInfoQueryRepository restaurantInfoQueryRepository,
        IMapper mapper
    ) : ResponseMaker, ICommandHandler<CreateOrUpdateInfoCommand>
    {
        public async Task<ResponseModel<bool>> Handle(CreateOrUpdateInfoCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                if (!await permissionService.Check(command.UserId, command.PermissionIds, command.UserRole,
                        cancellationToken))
                    return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
                var restaurantInfo =
                    await restaurantInfoQueryRepository.GetByRestaurantId(command.RestaurantId, cancellationToken);
                if (restaurantInfo is not null)
                {
                    restaurantInfo.LocationX = command.RestaurantInfoRequest.LocationX;
                    restaurantInfo.LocationY = command.RestaurantInfoRequest.LocationY;
                    restaurantInfo.PriceTypeId = command.RestaurantInfoRequest.PriceTypeId;
                    restaurantInfo.Address = command.RestaurantInfoRequest.Address;
                    restaurantInfo.AddressEng = command.RestaurantInfoRequest.AddressEng;
                    restaurantInfo.RegionId = command.RestaurantInfoRequest.RegionId;
                    restaurantInfo.BusinessTypeId = command.RestaurantInfoRequest.BusinessTypeId;
                    restaurantInfo.HasCoupe = command.RestaurantInfoRequest.HasCoupe;
                    restaurantInfo.HasTerrace = command.RestaurantInfoRequest.HasTerrace;
                    restaurantInfo.HallStartTime = command.RestaurantInfoRequest.HallStartTime;
                    restaurantInfo.HallEndTime = command.RestaurantInfoRequest.HallEndTime;
                    restaurantInfo.KitchenStartTime = command.RestaurantInfoRequest.KitchenStartTime;
                    restaurantInfo.KitchenEndTime = command.RestaurantInfoRequest.KitchenEndTime;
                    restaurantInfo.MusicStartTime = command.RestaurantInfoRequest.MusicStartTime;
                    restaurantInfo.MusicEndTime = command.RestaurantInfoRequest.MusicEndTime;
                    restaurantInfo.ForCorporateEvents = command.RestaurantInfoRequest.ForCorporateEvents;
                    restaurantInfo.DescriptionGeo = command.RestaurantInfoRequest.DescriptionGeo;
                    restaurantInfo.DescriptionEng = command.RestaurantInfoRequest.DescriptionEng;
                    restaurantInfo.ActiveStatusId = command.RestaurantInfoRequest.ActiveStatusId;
                    restaurantInfo.UpdateUserId = command.UserId;
                    var updateResponse = restaurantInfoCommandRepository.Update(restaurantInfo);
                    if (!updateResponse) return Fail(ErrorResponseType.RestaurantInfo.ErrorUpdate);
                }
                else
                {   
                    var infoEntity = mapper.Map<RestaurantInfoEntity>(command.RestaurantInfoRequest);
                    infoEntity.CreatedAt = DateTime.Now;
                    infoEntity.RestaurantId = command.RestaurantId;
                    infoEntity.UpdatedAt = DateTime.Now;
                    infoEntity.UpdateUserId = command.UserId;
                    var createResponse = await restaurantInfoCommandRepository.Create(infoEntity, cancellationToken);
                    if (!createResponse) return Fail(ErrorResponseType.RestaurantInfo.ErrorCreate);
                }

                var saveChangeResponse = await restaurantInfoCommandRepository.SaveChanges(cancellationToken);
                return saveChangeResponse ? Success() : Fail(ErrorResponseType.RestaurantInfo.ErrorSaveChanges);
            }
            catch (Exception ex)
            {
                return Unexpected(ex);
            }
        }
    }

}