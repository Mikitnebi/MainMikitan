using MainMikitan.Application.Services.Otp;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServicesAdapter.HandlerResponseMaker;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CustomerDeleteAccountVerifyOtpCommand(int customerId, string otp) : ICommand
{
    public int CustomerId { get; } = customerId;
    public string Otp { get; } = otp;
}

public class CustomerDeleteAccountVerifyOtpCommandHandler(
    ICustomerCommandRepository customerCommandRepository,
    ICustomerQueryRepository customerQueryRepository,
    IOtpCheckerService otpCheckerService)
    : ResponseMaker, ICommandHandler<CustomerDeleteAccountVerifyOtpCommand>
{
    public async Task<ResponseModel<bool>> Handle(CustomerDeleteAccountVerifyOtpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await customerQueryRepository.GetById(request.CustomerId);
            if (customer is null)
                return Fail(ErrorType.Customer.NotFound);
            var checkOtp = await otpCheckerService.CheckOtp(request.Otp, customer.EmailAddress, (int) Enums.OtpOperationTypeId.CustomerDeleteAccount);
            if (!checkOtp.Result) return checkOtp;
            customer.StatusId = (int)Enums.CustomerStatusId.TemporaryDeleted;
            var updateCustomer = await customerCommandRepository.CreateOrUpdate(customer);
            return !updateCustomer
                ? Fail(ErrorType.Customer.NotUpdated) 
                : Success();
        }
        catch (Exception ex)
        {
            return Unexpected(ex);
        }
    }
}