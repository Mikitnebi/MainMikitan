using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapterService.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MainMikitan.Application.Features.Customer.Commands;

public class AddOrUpdateProfilePhotoCommand(IFormFile command, int customerId) : ICommand
{
    public readonly IFormFile CustomerProfilePhoto = command;
    public int CustomerId { get; set; } = customerId;
}

public class AddOrUpdateProfilePhotoCommandHandler(IS3Adapter s3Adapter)
    : ResponseMaker, ICommandHandler<AddOrUpdateProfilePhotoCommand>
{
    public async Task<ResponseModel<bool>> Handle(AddOrUpdateProfilePhotoCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var customerProfilePhoto = request.CustomerProfilePhoto;
            var customerId = request.CustomerId;
                var updateRequest = await s3Adapter.AddOrUpdateCustomerProfileImage(customerProfilePhoto, customerId);
                return !updateRequest ? Fail(ErrorType.S3.ImageNotCreatedOrUpdated) : Success();
        }
        catch (Exception ex)
        {
            return Unexpected(ex.Message);
        }
    }
}