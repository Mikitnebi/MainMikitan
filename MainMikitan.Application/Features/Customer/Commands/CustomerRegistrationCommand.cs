using MainMikitan.Common.Validations;
using MainMikitan.Domain;
using MainMikitan.Domain.Interfaces.Customer;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Application.Features.Customer.Commands {
    public class CustomerRegistrationCommand : IRequest<ResponseModel<bool>> {
        public CustomerRegistrationRequest _registrationRequest { get; set; }
        public CustomerRegistrationCommand(CustomerRegistrationRequest model) {
            _registrationRequest = model;
        }
    }
    public class CustomorRegistrationCommandHandler : IRequestHandler<CustomerRegistrationCommand, ResponseModel<bool>> {
        private readonly ICustomerQueryRepository _customerQueryRepository;
        private readonly ICustomerCommandRepository _customerCommandRepository;
        public CustomorRegistrationCommandHandler(
            ICustomerQueryRepository customerQueryRepository,
            ICustomerCommandRepository customerCommandRepository
            ) {
            _customerCommandRepository = customerCommandRepository;
            _customerQueryRepository = customerQueryRepository;
        }


        public async Task<ResponseModel<bool>> Handle(CustomerRegistrationCommand request, CancellationToken cancellationToken) {
            var response = new ResponseModel<bool>();
            var registrationRequest = request._registrationRequest;
            try {
                var email = registrationRequest.Email;
                var validation = CustomerRequestsValidation.Registration(registrationRequest);
                if (validation.HasError) return validation;

                var createCustomerResult = await _customerCommandRepository.Create(new Domain.Models.Customer.CustomerEntity {
                    Email = email,
                    FullName = registrationRequest.FullName,
                    MobileNumber = registrationRequest.MobileNumber,
                    GenderId = registrationRequest.GenderId,
                    HashPassWord = registrationRequest.Password
                });
                return response;
            } catch (Exception ex) {
                response.ErrorType = ErrorType.UnExpectedException;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
    }
}
