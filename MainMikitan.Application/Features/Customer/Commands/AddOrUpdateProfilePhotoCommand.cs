using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapterService.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MainMikitan.Application.Features.Customer.Commands;

public class AddOrUpdateProfilePhotoCommand: ICommand
{
    public readonly IFormFile CustomerProfilePhoto;
    public int CustomerId { get; set; }

    public AddOrUpdateProfilePhotoCommand(IFormFile command, int customerId)
    {
        CustomerProfilePhoto = command;
        CustomerId = customerId;
    }
}

public class AddOrUpdateProfilePhotoCommandHandler : ICommandHandler<AddOrUpdateProfilePhotoCommand>
{
    private readonly IS3Adapter _s3Adapter;

    public AddOrUpdateProfilePhotoCommandHandler(
        IS3Adapter s3Adapter
        )
    {
        _s3Adapter = s3Adapter;
    }

    public async Task<ResponseModel<bool>> Handle(AddOrUpdateProfilePhotoCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        try
        {
            var customerProfilePhoto = request.CustomerProfilePhoto;
            var customerId = request.CustomerId;
            try
            {
                var updateRequest = await _s3Adapter.AddOrUpdateCustomerProfileImage(customerProfilePhoto, customerId);
            }
            catch (MainMikitanException ex)
            {
                response.ErrorType = ErrorType.S3.ImageNotCreatedOrUpdated;
                response.ErrorMessage = ex.Message;
                return response;
            }
            response.Result = true;
            return response;
        }
        catch (Exception ex) {
            response.ErrorType = ErrorType.UnExpectedException;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }
}