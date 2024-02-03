using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CreateOrUpdateCustomerInfoCommand(CreateOrUpdateCustomerInfoRequest command, int customerId) : ICommand
{
    public readonly CreateOrUpdateCustomerInfoRequest CustomerRequest = command;
    public int CustomerId { get; } = customerId;
}

public class CreateOrUpdateCustomerInfoCommandHandler(ICustomerInfoRepository customerInfoRepository)
    : ResponseMaker, ICommandHandler<CreateOrUpdateCustomerInfoCommand>
{
    public async Task<ResponseModel<bool>> Handle(CreateOrUpdateCustomerInfoCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var customerRequest = request.CustomerRequest;
            var customerId = request.CustomerId;
            if(DateOnly.FromDateTime(DateTime.Now.AddYears(-18))< customerRequest.BirthDate)
                return Fail(ErrorType.UserIsNotAdult);
            var updateRequest = await customerInfoRepository.CreateOrUpdate(customerRequest, customerId, cancellationToken);
            if (!updateRequest)
                return Fail(ErrorType.CustomerInfo.NotCreated);
            if (await customerInfoRepository.SaveChanges(cancellationToken))
                return Fail(ErrorType.CustomerInfo.NotDbSave);
            return Success();
        }
        catch (Exception ex)
        {
            return Unexpected(ex);
        }
    }
}