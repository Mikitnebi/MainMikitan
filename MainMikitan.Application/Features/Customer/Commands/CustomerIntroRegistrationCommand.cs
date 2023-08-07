using MainMikitan.Common.Validations;
using MainMikitan.Domain;
using MainMikitan.Domain.Interfaces.Customer;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.ExternalServicesAdapter.Email;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.ExternalServicesAdapter.Email.EmailSenderService;

namespace MainMikitan.Application.Features.Customer.Commands
{
    public class CustomerIntroRegistrationCommand : IRequest<ResponseModel<bool>>
    {
        public string _email { get; set; }
        public CustomerIntroRegistrationCommand(string email)
        {
            _email = email;
        }
        public class CustomerEmailSenderRegistrationCommandHandler : IRequestHandler<CustomerIntroRegistrationCommand, ResponseModel<bool>>
        {
            private readonly IEmailSenderService _emailSenderService;
            private readonly ICustomerQueryRepository _customerQueryRepository;
            private readonly ICustomerCommandRepository _customerCommandRepository;
            public CustomerEmailSenderRegistrationCommandHandler(
                IEmailSenderService emailSenderService,
                ICustomerQueryRepository customerQueryRepository,
                ICustomerCommandRepository customerCommandRepository
                )
            {
                _emailSenderService = emailSenderService;
                _customerCommandRepository = customerCommandRepository;
                _customerQueryRepository = customerQueryRepository;
            }


            public async Task<ResponseModel<bool>> Handle(CustomerIntroRegistrationCommand request, CancellationToken cancellationToken)
            {
                var response = new ResponseModel<bool>();
                var email = request._email;
                var link = "...";
                var emailBuilder = new EmailBuilder();
                emailBuilder.AddReplacement("{{link}}", link);
                var emailSenderResult = await _emailSenderService.SendEmailAsync(email, emailBuilder, EmailType.CustomerRegistrationEmail);
                response.Result = true;
                return response;
            }
        }
    }
}
