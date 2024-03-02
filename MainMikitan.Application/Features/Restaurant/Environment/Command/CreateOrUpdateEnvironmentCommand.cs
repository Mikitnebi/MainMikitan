using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Restaurant.Environment.Command {
    public class CreateOrUpdateEnvironmentCommand : ICommand
    {
        public int UserId { get; set; }
        public List<int> EnvironmentIds { get; set; } = [];
        public IEnumerable<int> PermissionIds { get; set; } 
        public int RestaurantId { get; set; }
        public string UserRole { get; set; }
        public CreateOrUpdateEnvironmentCommand(
            RestaurantRegistrationEnvironmentRequest request,
            int userId,
            string userRole,
            int restaurantId,
            IEnumerable<int> permissionIds)
        {
            RestaurantId = restaurantId;
            UserId = userId;
            UserRole = userRole;
            PermissionIds = permissionIds;
            EnvironmentIds.AddRange(request.EnvironmentTypeIds);
            EnvironmentIds.AddRange(request.CousinsTypeIds);
            EnvironmentIds.AddRange(request.MusicsTypeIds);
        }
    }

    public class CreateOrUpdateEnvironmentCommandHandler(
        IRestaurantEnvCommandRepository restaurantEnvCommandRepository,
        IRestaurantEnvQueryRepository restaurantEnvQueryRepository,
        IPermissionService permissionService
        )
        : ResponseMaker, ICommandHandler<CreateOrUpdateEnvironmentCommand>
    {
        public async Task<ResponseModel<bool>> Handle(CreateOrUpdateEnvironmentCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                if (!await permissionService.Check(command.UserId, command.PermissionIds, command.UserRole,
                        cancellationToken))
                    return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
                
                var environmentIds = command.EnvironmentIds;
                var restaurantId = command.RestaurantId;

                var restaurantCurrentInterests = await restaurantEnvQueryRepository.Get(restaurantId, cancellationToken);
                if (restaurantCurrentInterests.Count != 0)
                {
                    var deleteResponse = await restaurantEnvCommandRepository.Delete(restaurantId, cancellationToken);
                    if (!deleteResponse)
                        return Fail(ErrorResponseType.RestaurantEnvironment.ErrorDelete);
                }

                var addResponse = await restaurantEnvCommandRepository.Add(environmentIds, restaurantId, cancellationToken);
                if (!addResponse)
                    return Fail(ErrorResponseType.RestaurantEnvironment.ErrorAdd);
                if (!(await restaurantEnvCommandRepository.SaveChanges(cancellationToken)))
                    return Fail(ErrorResponseType.RestaurantEnvironment.ErrorDbSave);
                return Success();
            }
            catch (Exception ex)
            {
                return Unexpected(ex);
            }
        }
    }
}
