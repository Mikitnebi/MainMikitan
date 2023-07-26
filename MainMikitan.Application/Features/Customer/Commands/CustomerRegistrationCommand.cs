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

namespace MainMikitan.Application.Features.Customer.Commands
{
    public class CustomerRegistrationCommand : IRequest<ResponseModel<bool>>
    {
        public CustomerRegistrationRequest _registrationRequest { get; set; }
        public CustomerRegistrationCommand(CustomerRegistrationRequest model)
        {
            _registrationRequest = model;
        }
        public class CustomorRegistrationCommandHandler : IRequestHandler<CustomerRegistrationCommand, ResponseModel<bool>>
        {
            private readonly ICustomerQueryRepository _customerQueryRepository;
            public CustomorRegistrationCommandHandler(
                ICustomerQueryRepository customerQueryRepository
                )
            {
                _customerQueryRepository = customerQueryRepository;
            }


            public Task<ResponseModel<bool>> Handle(CustomerRegistrationCommand request, CancellationToken cancellationToken)
            {

            }
        }
    }
}
