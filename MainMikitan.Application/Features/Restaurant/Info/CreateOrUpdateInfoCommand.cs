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
    public class CreateOrUpdateInfoCommand(RestaurantInfoRequest request, int userId, int roleId, int restaurantId, IEnumerable<int> permissionIds) : ICommand
        {
        public RestaurantInfoRequest RestaurantInfoRequest { get; set; } = request;
        public int UserId { get; set; } = userId;
        public int RoleId { get; set; } = roleId;
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
            if(await permissionService.Check(command.UserId, command.PermissionIds, command.RoleId, cancellationToken))
                return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
            var infoEntity = mapper.Map<RestaurantInfoEntity>(command.RestaurantInfoRequest);
            var restaurantInfo = await restaurantInfoQueryRepository.GetByRestaurantId(command.RestaurantId, cancellationToken);
            if (restaurantInfo.Id == 0)
            {
                infoEntity.Id = restaurantInfo.Id;
                infoEntity.RestaurantId = command.RestaurantId;
                infoEntity.UpdateUserId = command.UserId;
                var updateResponse = restaurantInfoCommandRepository.Update(infoEntity);
                if (!updateResponse) return Fail(ErrorResponseType.RestaurantInfo.ErrorUpdate);
            }
            else
            {
                infoEntity.CreatedAt = DateTime.Now;
                infoEntity.RestaurantId = command.RestaurantId;
                infoEntity.UpdatedAt = DateTime.Now;
                infoEntity.UpdateUserId = command.UserId;
                var createResponse = await restaurantInfoCommandRepository.Create(infoEntity, cancellationToken);
                if (!createResponse) return Fail(ErrorResponseType.RestaurantInfo.ErrorCreate);
            }
            var saveChangeResponse = await restaurantInfoCommandRepository.SaveChanges(cancellationToken);
            return saveChangeResponse ? Success() :
                Fail(ErrorResponseType.RestaurantInfo.ErrorSaveChanges);
        }
    }

}