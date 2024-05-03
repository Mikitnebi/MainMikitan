using MainMikitan.Application.Services.Permission;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.ErrorResponseType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MainMikitan.Application.Features.Restaurant.Environment.Command
{
    public class CreateOrUpdateEnvironmentImagesCommand(List<IFormFile> images, int userId, string userRole, int restaurantId, IEnumerable<int> permissionIds) : ICommand
    {
        public List<IFormFile> Images { get; set; } = images;
        public int UserId { get; set; } = userId;
        public string UserRole { get; set; } = userRole;
        public int RestaurantId { get; set;} = restaurantId;
        public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    }

   public class CreateOrUpdateEnvironmentImagesCommandHandler(
        IPermissionService permissionService,
        IS3Adapter s3Adapter
        ) : ResponseMaker, ICommandHandler<CreateOrUpdateEnvironmentImagesCommand>
    {
        public async Task<ResponseModel<bool>> Handle(CreateOrUpdateEnvironmentImagesCommand request, CancellationToken cancellationToken)
        {
            if (!await permissionService.Check(request.UserId, request.PermissionIds, request.UserRole,
            cancellationToken))
                return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
            var baseKey = $"Restaurant/{request.RestaurantId}/Environment/";
            var deleteResponse = await s3Adapter.DeleteAllContentWithKey(baseKey);
            //todo
            if (deleteResponse.HasError) return Fail(ErrorResponseType.S3.UnexpectedException);
            var updateRequest = await s3Adapter.AddRestaurantEnvironmentImage(request.Images, request.RestaurantId);
            return !updateRequest ? Fail(ErrorResponseType.S3.ImageNotCreatedOrUpdated) : Success();
        }
    }
}
