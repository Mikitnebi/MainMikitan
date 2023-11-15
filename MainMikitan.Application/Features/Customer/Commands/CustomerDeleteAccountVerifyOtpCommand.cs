using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CustomerDeleteAccountVerifyOtpCommand : ICommand
{
    public int CustomerId { get; }
    public string CustomerEmail { get; }
    public string Otp { get; }
    public CustomerDeleteAccountVerifyOtpCommand(int customerId, string customerEmail, string otp)
    {
        CustomerId = customerId;
        CustomerEmail = customerEmail;
        Otp = otp;
    }
}

public class CustomerDeleteAccountVerifyOtpCommandHandler : ICommandHandler<CustomerDeleteAccountVerifyOtpCommand>
{
    private readonly IS3Adapter _s3Adapter;
    private readonly IOtpLogQueryRepository _otpLogQueryRepository;
    private readonly IOtpLogCommandRepository _otpLogCommandRepository;
    private readonly ICustomerInfoRepository _customerInfoRepository;
    private readonly ICustomerInterestRepository _customerInterestRepository;
    private readonly ICustomerCommandRepository _customerCommandRepository;
    public CustomerDeleteAccountVerifyOtpCommandHandler(
        IOtpLogQueryRepository otpLogQueryRepository, 
        IOtpLogCommandRepository otpLogCommandRepository, 
        ICustomerCommandRepository customerCommandRepository, ICustomerInfoRepository customerInfoRepository, ICustomerInterestRepository customerInterestRepository, IS3Adapter s3Adapter)
    {
        _otpLogQueryRepository = otpLogQueryRepository;
        _otpLogCommandRepository = otpLogCommandRepository;
        _customerCommandRepository = customerCommandRepository;
        _customerInfoRepository = customerInfoRepository;
        _customerInterestRepository = customerInterestRepository;
        _s3Adapter = s3Adapter;
    }
    public async Task<ResponseModel<bool>> Handle(CustomerDeleteAccountVerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        try
        {
            var otp = await _otpLogQueryRepository.GetOtpbyEmail(request.CustomerEmail);
            if (otp is null)
            {
                response.ErrorType = ErrorType.Otp.IncorrectEmail;
                return response;
            }

            if (otp.StatusId == (int)Enums.OtpStatusId.Success)
            {
                response.ErrorType = ErrorType.Otp.AlreadyUsedOtp;
                return response;
            }

            if (DateTime.Now > otp.CreatedAt.AddMinutes(otp.ValidationTime) ||
                otp.StatusId == (int)Enums.OtpStatusId.NotValid)
            {
                response.ErrorType = ErrorType.Otp.NotValidOtp;
                return response;
            }

            if (otp.Otp != request.Otp)
            {
                response.ErrorType = ErrorType.Otp.NotCorrectOtp;
                return response;
            }

            var otpUpdate = await _otpLogCommandRepository.Update(otp.Id, 0, (int)Enums.OtpStatusId.Success);
            if (otpUpdate is null or 0)
            {
                response.ErrorType = ErrorType.Otp.OtpNotUpdated;
                return response;
            }

            var executeDeleteItems = new List<Task<bool>>()
            {

            };
            var deleteCustomer = await _customerCommandRepository.Delete(request.CustomerId);
            var deleteCustomerInfo = await _customerInfoRepository.Delete(request.CustomerId);
            var deleteCustomerInterests = await _customerInterestRepository.Delete((request.CustomerId));
            //var deleteCustomerProfilePhoto = await _s3Adapter.DeleteCustomerPhoto(request.CustomerId);
           // if (customerDelete is null or 0)
           // {
            //    response.ErrorType = ErrorType.Customer.NotDeleted;
            //    return response;
            //}

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