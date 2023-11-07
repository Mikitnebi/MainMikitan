using MainMikitan.Database.Features.Category.Query;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServicesAdapter.Validations;
using MediatR;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CreateOrUpdateCustomerInfoCommand : ICommand
{
    public readonly CreateOrUpdateCustomerInfoRequest CustomerRequest;
    public int CustomerId { get; set; }

    public CreateOrUpdateCustomerInfoCommand(CreateOrUpdateCustomerInfoRequest command, int customerId)
    {
        CustomerRequest = command;
        CustomerId = customerId;
    }
}

public class CreateOrUpdateCustomerInfoCommandHandler : ICommandHandler<CreateOrUpdateCustomerInfoCommand>
{
    private readonly ICustomerInfoRepository _customerInfoRepository;

    public CreateOrUpdateCustomerInfoCommandHandler(
        ICustomerInfoRepository customerInfoRepository
        )
    {
        _customerInfoRepository = customerInfoRepository;
    }

    public async Task<ResponseModel<bool>> Handle(CreateOrUpdateCustomerInfoCommand request,
        CancellationToken cancellationToken)
    {
        var customerRequest = request.CustomerRequest;
        var customerId = request.CustomerId;
        var response = new ResponseModel<bool>();
        var updateRequest = await _customerInfoRepository.CreateOrUpdate(customerRequest, customerId);
        if (!updateRequest)
        {
            response.ErrorType = ErrorType.CustomerInfo.NotCreated;
            return response;
        }

        if (await _customerInfoRepository.SaveChanges())
        {
            response.ErrorType = ErrorType.CustomerInfo.NotDbSave;
            return response;
        }
        response.Result = true;
        return response;
    }
}