
using MainMikitan.Application.Services.Otp;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer.Feature;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServiceAdapter.Validations;

namespace MainMikitan.Application.Features.Customer.Commands;

public abstract class CustomerPasswordResetVerifyOtpCommand(CustomerPasswordResetVerifyOtpModel model, int customerId) : ICommand
{
    public readonly string Otp = model.Otp;
    public readonly string Password = model.Password;
    public readonly int CustomerId = customerId;
}

public class CustomerPasswordResetVerifyOtpCommandHandler(
    ICustomerQueryRepository customerQueryRepository,
    ICustomerCommandRepository customerCommandRepository,
    IOtpCheckerService otpCheckerService
    )
    : ResponseMaker, ICommandHandler<CustomerPasswordResetVerifyOtpCommand>
{
    public async Task<ResponseModel<bool>> Handle(CustomerPasswordResetVerifyOtpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var passwordCheckResult = CustomerRequestsValidation.PasswordCheck(request.Password);
            if ((short)passwordCheckResult < 3)
                return Fail(ErrorType.NotCorrectPasswordType);
            var customer = await customerQueryRepository.GetById(request.CustomerId);
            if (customer is null)
                return Fail(ErrorType.Customer.NotFound);
            var checkOtp = await otpCheckerService.CheckOtp(request.Otp, customer.EmailAddress, (int)Enums.OtpOperationTypeId.CustomerPasswordReset);
            if (!checkOtp.Result) return checkOtp;
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