using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Templates;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Application.Features.Customer.Commands
{
    public class CustomerRegistrationVerifyOtpCommand : ICommand
    {
        public string _email { get; set; }
        public string _otp { get; set; }
        public CustomerRegistrationVerifyOtpCommand(GeneralRegistrationVerifyOtpRequest request)
        {
            _email = request.Email;
            _otp = request.Otp;
        }
    }
    public class CustomerRegistrationVerifyOtpcommandHandler : ICommandHandler<CustomerRegistrationVerifyOtpCommand>
    {
        private readonly IOtpLogQueryRepository _otpLogQueryRepository;
        private readonly IOtpLogCommandRepository _otpLogCommandRepository;
        private readonly ICustomerCommandRepository _customerCommandRepository;

        public CustomerRegistrationVerifyOtpcommandHandler(
            IOtpLogQueryRepository otpLogQueryRepository,
            IOtpLogCommandRepository otpLogCommandRepository,
            ICustomerCommandRepository customerCommandRepository)
        {
            _otpLogCommandRepository = otpLogCommandRepository;
            _otpLogQueryRepository = otpLogQueryRepository;
            _customerCommandRepository = customerCommandRepository;
        }
        public async Task<ResponseModel<bool>> Handle (CustomerRegistrationVerifyOtpCommand model, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<bool>();
            var otp = await _otpLogQueryRepository.GetOtpByEmail(model._email, (int) Enums.OtpOperationTypeId.CustomerRegistration);
            if (otp is null) 
            {
                response.ErrorType = ErrorType.Otp.IncorrectEmail;
                return response;
            }
            if(otp.StatusId == (int)OtpStatusId.Success)
            {
                response.ErrorType = ErrorType.Otp.AlreadyUsedOtp;
                return response;
            }
            if(DateTime.Now > otp.CreatedAt.AddMinutes(otp.ValidationTime) || otp.StatusId == (int) OtpStatusId.NotValid)
            {
                response.ErrorType = ErrorType.Otp.NotValidOtp;
                return response;
            }
            if(otp.Otp != model._otp)
            {
                response.ErrorType = ErrorType.Otp.NotCorrectOtp;
                return response;
            }
            var otpUpdate = await _otpLogCommandRepository.Update(otp.Id, 0, (int)OtpStatusId.Success);
            if(!otpUpdate) 
            {
                response.ErrorType = ErrorType.Otp.OtpNotUpdated;
                return response;
            }
            var customerUpdate = await _customerCommandRepository.UpdateStatus(model._email, true, CustomerStatusId.FullyVerified);
            if (!customerUpdate)
            {
                response.ErrorType = ErrorType.Customer.NotUpdated;
                return response;
            }
            response.Result = true;
            return response;
        }
    }
}
