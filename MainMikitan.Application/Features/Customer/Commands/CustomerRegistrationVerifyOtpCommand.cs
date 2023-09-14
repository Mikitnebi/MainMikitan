﻿using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain;
using MainMikitan.Domain.Interfaces.Customer;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.GeneralRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Application.Features.Customer.Commands
{
    public class CustomerRegistrationVerifyOtpCommand : IRequest<ResponseModel<bool>>
    {
        public string _email { get; set; }
        public string _otp { get; set; }
        public CustomerRegistrationVerifyOtpCommand(GeneralRegistrationVerifyOtpRequest request)
        {
            _email = request.Email;
            _otp = request.Otp;
        }
    }
    public class CustomerRegistrationVerifyOtpcommandHandler : IRequestHandler<CustomerRegistrationVerifyOtpCommand, ResponseModel<bool>>
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
            var otp = await _otpLogQueryRepository.GetOtpbyEmail(model._email);
            if (otp == null) 
            {
                response.ErrorType = ErrorType.EmailNotFound;
                return response;
            }
            if(otp.StatusId == (int)OtpStatusId.Success)
            {
                response.ErrorType = ErrorType.AlreadyUsedOtp;
                return response;
            }
            if(DateTime.Now > otp.CreatedAt.AddMinutes(otp.ValidationTime) || otp.StatusId == (int) OtpStatusId.NotValid)
            {
                response.ErrorType = ErrorType.NotValidOtp;
                return response;
            }
            if(otp.Otp != model._otp)
            {
                response.ErrorType = ErrorType.NotCorrectOtp;
                return response;
            }
            var otpUpdate = await _otpLogCommandRepository.Update(otp.Id, 0, OtpStatusId.Success);
            if(otpUpdate == null || otpUpdate == 0) 
            {
                response.ErrorType = ErrorType.OtpNotUpdated;
                return response;
            }
            var customerUpdate = await _customerCommandRepository.UpdateStatus(model._email, true, CustomerStatusId.Verified);
            if (customerUpdate == null || customerUpdate == 0)
            {
                response.ErrorType = ErrorType.CustomerNotUpdated;
                return response;
            }
            response.Result = true;
            return response;
        }
    }
}