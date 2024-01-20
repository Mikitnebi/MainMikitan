using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.InternalServicesAdapter.HandlerResponseMaker;

namespace MainMikitan.Application.Services.Otp;

public class OtpCheckerService(
    IOtpLogQueryRepository otpLogQueryRepository,
    IOtpLogCommandRepository otpLogCommandRepository) : IOtpCheckerService
{
    public async Task<ResponseModel<bool>> CheckOtp(string userOtp, string email)
    {
        var otp = await otpLogQueryRepository.GetOtpByEmail(email);
        if (otp is null)
            return HandlerResponseMakerService.NewFailedResponse(ErrorType.Otp.IncorrectEmail);
        if (otp.StatusId == (int)Enums.OtpStatusId.Success)
            return HandlerResponseMakerService.NewFailedResponse(ErrorType.Otp.AlreadyUsedOtp);
        if (DateTime.Now > otp.CreatedAt.AddMinutes(otp.ValidationTime) ||
            otp.StatusId == (int)Enums.OtpStatusId.NotValid)
            return HandlerResponseMakerService.NewFailedResponse(ErrorType.Otp.NotValidOtp);
        if (otp.Otp != userOtp)
        {
            var updateOtpTrials = await otpLogCommandRepository.Update(otp.Id, otp.NumberOfTrials + 1, otp.NumberOfTrials);
            return HandlerResponseMakerService.NewFailedResponse(updateOtpTrials is null or 0 
                ? ErrorType.Otp.OtpNotUpdated 
                : ErrorType.Otp.NotCorrectOtp);
        }
        var otpUpdate = await otpLogCommandRepository.Update(otp.Id, otp.NumberOfTrials, (int)Enums.OtpStatusId.Success);
        return otpUpdate is null or 0 
            ? HandlerResponseMakerService.NewFailedResponse(ErrorType.Otp.OtpNotUpdated) 
            : HandlerResponseMakerService.NewSucceedResponse();
    }
}