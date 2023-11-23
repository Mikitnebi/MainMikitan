﻿using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Requests.Customer.Auth;
using MainMikitan.InternalServiceAdapter.Auth;
using MediatR;

namespace MainMikitan.Application.Features.Customer.Commands
{
    public class CustomerLoginCommand : IRequest<ResponseModel<AuthTokenResponseModel>>
    {
        public string _EmailAddress { get; set; }
        public string _Password { get; set; }
        public CustomerLoginCommand(CustomerLoginRequest request) 
        {
            _EmailAddress = request.EmailAddress;
            _Password = request.Password;
        }
    }
    public class CustomerLoginCommandHandler : IRequestHandler<CustomerLoginCommand, ResponseModel<AuthTokenResponseModel>>
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;
        private readonly IAuthService _authService;
        private readonly IPasswordHasher _passwordHasher;
        public CustomerLoginCommandHandler
            (
            ICustomerQueryRepository customerQueryRepository,
            IPasswordHasher passwordHasher,
            IAuthService authService)
        {
            _customerQueryRepository = customerQueryRepository;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }
        public async Task<ResponseModel<AuthTokenResponseModel>> Handle (CustomerLoginCommand command, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<AuthTokenResponseModel>();
            var email = command._EmailAddress;
            var password = command._Password;
            var customer = await _customerQueryRepository.GetByEmail(email);
            if(customer == null || !_passwordHasher.VerifyPassword(password, customer.HashPassWord))
            {
                response.ErrorType = ErrorType.NotCorrectEmailOrPassword;
                return response;
            }
            response = _authService.CustomerAuth(new CustomerAuthRequestModel
            {
                EmailAdress = email,
                Id = customer.Id
            });
            return response;
        }
    }
}
