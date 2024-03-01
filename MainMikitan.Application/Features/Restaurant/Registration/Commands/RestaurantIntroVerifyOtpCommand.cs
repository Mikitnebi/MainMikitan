using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Restaurant.Command;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.GeneralRequests;
using MediatR;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Application.Features.Customer.Commands {
    public class RestaurantIntroVerifyOtpCommand(GeneralRegistrationVerifyOtpRequest request)
        : IRequest<ResponseModel<bool>>
    {
        public string _email { get; set; } = request.Email;
        public string _otp { get; set; } = request.Otp;
    }
    public class RestaurantIntroVerifyOtpCommandHandler(
        IOtpLogQueryRepository otpLogQueryRepository,
        IOtpLogCommandRepository otpLogCommandRepository,
        IRestaurantIntroCommandRepository restaurantIntroCommandRepository)
        : IRequestHandler<RestaurantIntroVerifyOtpCommand, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(RestaurantIntroVerifyOtpCommand model, CancellationToken cancellationToken) {
            var response = new ResponseModel<bool>();
            var otp = await otpLogQueryRepository.GetOtpByEmail(model._email, (int)Enums.OtpOperationTypeId.RestaurantIntroRegistration);
            if (otp == null) {
                response.ErrorType = ErrorResponseType.Otp.IncorrectEmail;
                return response;
            }
            if (otp.StatusId == (int)OtpStatusId.Success) {
                response.ErrorType = ErrorResponseType.Otp.AlreadyUsedOtp;
                return response;
            }
            if (DateTime.Now > otp.CreatedAt.AddMinutes(otp.ValidationTime) || otp.StatusId == (int)OtpStatusId.NotValid) {
                response.ErrorType = ErrorResponseType.Otp.NotValidOtp;
                return response;
            }
            if (otp.Otp != model._otp) {
                response.ErrorType = ErrorResponseType.Otp.NotCorrectOtp;
                return response;
            }
            var otpUpdate = await otpLogCommandRepository.Update(otp.Id, 0, (int)OtpStatusId.Success, cancellationToken);
            if (!otpUpdate|| !otpUpdate) {
                response.ErrorType = ErrorResponseType.Otp.OtpNotUpdated;
                return response;
            }
            var restaurantUpdate = await restaurantIntroCommandRepository.UpdateStatus(model._email, true, RestaurantOtpVerificationId.Verified);
            if (restaurantUpdate == null || restaurantUpdate == 0) {
                response.ErrorType = ErrorResponseType.Restaurant.NotUpdated;
                return response;
            }
            response.Result = true;
            return response;
        }
    }
}
