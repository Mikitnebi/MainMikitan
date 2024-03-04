using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Templates;
using MainMikitan.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;
using MainMikitan.Database.Features.LegalEntity.Interface;

namespace MainMikitan.Application.Features.LegalEntity.Commands
{
    public class LegalEntityIntroVerifyOtpCommand : ICommand
    {
        public string _email { get; set; }
        public string _otp { get; set; }
        public LegalEntityIntroVerifyOtpCommand(GeneralRegistrationVerifyOtpRequest request)
        {
            _email = request.Email;
            _otp = request.Otp;
        }
    }
    public class LegalEntityIntroVerifyOtpCommandHandler(
            IOtpLogQueryRepository otpLogQueryRepository,
            IOtpLogCommandRepository otpLogCommandRepository,
            ILegalEntityIntroCommandRepository legalEntityIntroCommandRepository) : ResponseMaker, ICommandHandler<LegalEntityIntroVerifyOtpCommand>
    {
        public async Task<ResponseModel<bool>> Handle(LegalEntityIntroVerifyOtpCommand model, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<bool>();
            var otp = await otpLogQueryRepository.GetOtpByEmail(model._email, (int)Enums.OtpOperationTypeId.LegalEntityIntroRegistration);
            if (otp is null)
            {
                return Fail(ErrorResponseType.Otp.IncorrectEmail);
            }
            if (otp.StatusId == (int)OtpStatusId.Success)
            {
                return Fail(ErrorResponseType.Otp.AlreadyUsedOtp);
            }
            if (DateTime.Now > otp.CreatedAt.AddMinutes(otp.ValidationTime) || otp.StatusId == (int)OtpStatusId.NotValid)
            {
                return Fail(ErrorResponseType.Otp.NotValidOtp);
            }
            if (otp.Otp != model._otp)
            {
                return Fail(ErrorResponseType.Otp.NotCorrectOtp);
            }
            var otpUpdate = await otpLogCommandRepository.Update(otp.Id, 0, (int)OtpStatusId.Success);
            if (!otpUpdate)
            {
                return Fail(ErrorResponseType.Otp.OtpNotUpdated);
            }
            var legalEntityUpdate = await legalEntityIntroCommandRepository.UpdateStatus(model._email, true, LegalEntityOtpVerificationId.Verified);
            if (!legalEntityUpdate)
            {
                return Fail(ErrorResponseType.Customer.NotUpdated);
            }
            return Success();
        }
    }
}
