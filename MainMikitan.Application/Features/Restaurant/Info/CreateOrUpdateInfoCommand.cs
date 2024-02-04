using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Templates;
using MediatR;

namespace MainMikitan.Application.Features.Restaurant.Info {
    public class CreateOrUpdateInfoCommand(RestaurantInfoRequest request, int userId, int roleId, int permissionId) : ICommand
        {
        public RestaurantInfoRequest RestaurantInfoRequest { get; set; } = request;
        public int UserId { get; set; } = userId;
        public int RoleId { get; set; } = roleId;
        public int PermissionId { get; set; } = permissionId;
    }
    public class CreateOrUpdateInfoCommandHandler : ResponseMaker, ICommandHandler<CreateOrUpdateInfoCommand> {
        public async Task<ResponseModel<bool>> Handle(CreateOrUpdateInfoCommand command, CancellationToken cancellationToken) {
            
            return Fail("");
        }
    }

}