using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Restaurant.Command;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.GeneralRequests;
using MediatR;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Application.Features.Customer.Commands {
    public class RestaurantIntroVerifyOtpCommand : IRequest<ResponseModel<bool>> {
        public string _email { get; set; }
        public string _otp { get; set; }
        public RestaurantIntroVerifyOtpCommand(GeneralRegistrationVerifyOtpRequest request) {
            _email = request.Email;
            _otp = request.Otp;
        }
    }
    public class RestaurantIntroVerifyOtpCommandHandler : IRequestHandler<RestaurantIntroVerifyOtpCommand, ResponseModel<bool>> {
        private readonly IOtpLogQueryRepository _otpLogQueryRepository;
        private readonly IOtpLogCommandRepository _otpLogCommandRepository;
        private readonly IRestaurantIntroCommandRepository _restaurantIntroCommandRepository;

        public RestaurantIntroVerifyOtpCommandHandler(
            IOtpLogQueryRepository otpLogQueryRepository,
            IOtpLogCommandRepository otpLogCommandRepository,
            IRestaurantIntroCommandRepository restaurantIntroCommandRepository) {
            _otpLogCommandRepository = otpLogCommandRepository;
            _otpLogQueryRepository = otpLogQueryRepository;
            _restaurantIntroCommandRepository = restaurantIntroCommandRepository;
        }
        public async Task<ResponseModel<bool>> Handle(RestaurantIntroVerifyOtpCommand model, CancellationToken cancellationToken) {
            var response = new ResponseModel<bool>();
            var otp = await _otpLogQueryRepository.GetOtpbyEmail(model._email);
            if (otp == null) {
                response.ErrorType = ErrorType.EmailNotFound;
                return response;
            }
            if (otp.StatusId == (int)OtpStatusId.Success) {
                response.ErrorType = ErrorType.AlreadyUsedOtp;
                return response;
            }
            if (DateTime.Now > otp.CreatedAt.AddMinutes(otp.ValidationTime) || otp.StatusId == (int)OtpStatusId.NotValid) {
                response.ErrorType = ErrorType.NotValidOtp;
                return response;
            }
            if (otp.Otp != model._otp) {
                response.ErrorType = ErrorType.NotCorrectOtp;
                return response;
            }
            var otpUpdate = await _otpLogCommandRepository.Update(otp.Id, 0, OtpStatusId.Success);
            if (otpUpdate == null || otpUpdate == 0) {
                response.ErrorType = ErrorType.OtpNotUpdated;
                return response;
            }
            var restaurantUpdate = await _restaurantIntroCommandRepository.UpdateStatus(model._email, true, RestaurantOtpVerificationId.Verified);
            if (restaurantUpdate == null || restaurantUpdate == 0) {
                response.ErrorType = ErrorType.Restaurant.RestaurantNotUpdated;
                return response;
            }
            response.Result = true;
            return response;
        }
    }
}
