using MainMikitan.Domain.Models.Commons;

namespace MainMikitan.Application.Services.Otp;

public interface IOtpCheckerService
{ 
    Task<ResponseModel<bool>> CheckOtp(string customerOtp, string email);
}