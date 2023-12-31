using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Templates;

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
        var response = new ResponseModel<bool>();
        try
        {
            var customerRequest = request.CustomerRequest;
            var customerId = request.CustomerId;
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
        catch (Exception ex) {
            response.ErrorType = ErrorType.UnExpectedException;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }
}