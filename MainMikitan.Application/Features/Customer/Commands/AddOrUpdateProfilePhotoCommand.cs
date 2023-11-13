using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MediatR;
using Microsoft.AspNetCore.Http;
using static System.String;

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
    private readonly ICustomerInfoRepository _customerInfoRepository;
    private readonly IS3Adapter _s3Adapter;

    public AddOrUpdateProfilePhotoCommandHandler(
        ICustomerInfoRepository customerInfoRepository, 
        IS3Adapter s3Adapter
        )
    {
        _customerInfoRepository = customerInfoRepository;
        _s3Adapter = s3Adapter;
    }

    public async Task<ResponseModel<bool>> Handle(AddOrUpdateProfilePhotoCommand request,
        CancellationToken cancellationToken)
    {
        var customerProfilePhoto = request.CustomerProfilePhoto;
        var customerId = request.CustomerId;
        var response = new ResponseModel<bool>();
        var updateRequest = await _s3Adapter.AddOrUpdateCustomerProfileImage(customerProfilePhoto, customerId);
        if (updateRequest.HasError)
        {
            response.ErrorType = updateRequest.ErrorType;
            return response;
        }
        response.Result = true;
        return response;
    }
}