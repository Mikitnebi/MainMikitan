using AutoMapper;
using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Rating;
using MainMikitan.Domain.Requests.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Commands;

public class SaveCustomerRatingCommand(int restaurantId, int userId, SaveCustomerRatingRequest request, string userRole, IEnumerable<int> permissionIds) : ICommand<bool>
{
    public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    public string UserRole { get; set; } = userRole;
    public int RestaurantId { get; set; } = restaurantId;
    public int UserId { get; set; } = userId;
    public SaveCustomerRatingRequest Request { get; set; } = request;
}

public class SaveCustomerRatingCommandHandler(ICustomerRatingCommandRepository customerRatingCommandRepository,
    IPermissionService permissionService,
    IMapper mapper)
    : ResponseMaker, ICommandHandler<SaveCustomerRatingCommand, bool>
{
    public async Task<ResponseModel<bool>> Handle(SaveCustomerRatingCommand command,CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(command.UserId, command.PermissionIds, command.UserRole,
                cancellationToken, command.RestaurantId, 2))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var customerRatingEntity = mapper.Map<CustomerRatingEntity>(command.Request);
        customerRatingEntity.UserId = command.UserId;
        customerRatingEntity.RestaurantId = command.RestaurantId;
        customerRatingEntity.CreatedAt = DateTime.Now;
        
        await customerRatingCommandRepository.SaveRating(customerRatingEntity);
        return await customerRatingCommandRepository.SaveChangesAsync() ? Success() : Fail(ErrorResponseType.Rating.RatingSaveFail);
    }
}