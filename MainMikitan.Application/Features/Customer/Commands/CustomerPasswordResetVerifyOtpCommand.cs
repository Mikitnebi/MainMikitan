
using MainMikitan.Application.Services.Otp;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer.Feature;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServiceAdapter.Validations;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CustomerPasswordResetVerifyOtpCommand(CustomerPasswordResetVerifyOtpModel model, int customerId) : ICommand
{
    public string Otp { get; set; } = model.Otp;
    public string Password { get; set; } = model.Password;
    public int CustomerId { get; set; } = customerId;
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
            if ((short)passwordCheckResult < 2)
                return Fail(ErrorType.NotCorrectPasswordType);
            var customer = await customerQueryRepository.GetById(request.CustomerId, cancellationToken);
            if (customer is null)
                return Fail(ErrorType.Customer.NotFound);
            var checkOtp = await otpCheckerService.CheckOtp(request.Otp, customer.EmailAddress, (int)Enums.OtpOperationTypeId.CustomerPasswordReset, cancellationToken);
            if (!checkOtp.Result) return checkOtp;
            customer.HashPassWord = request.Password;
            var updateCustomer = customerCommandRepository.UpdateCustomer(customer);
            
            return !(await customerCommandRepository.SaveChanges(cancellationToken))
                ? Fail(ErrorType.Customer.NotUpdated) 
                : Success();
        }
        catch (Exception ex)
        {
            return Unexpected(ex);
        }
    }
}