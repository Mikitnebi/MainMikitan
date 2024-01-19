using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Requests.Customer.Auth;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServiceAdapter.Auth;
using Microsoft.AspNetCore.Identity;

namespace MainMikitan.Application.Features.Customer.Commands
{
    public class CustomerLoginCommand(CustomerLoginRequest request) : IQuery<AuthTokenResponseModel>
    {
        public string EmailAddress { get; } = request.EmailAddress;
        public string Password { get; } = request.Password;
    }
    public class CustomerLoginCommandHandler(
        ICustomerQueryRepository customerQueryRepository,
        IAuthService authService)
        : ResponseMaker<AuthTokenResponseModel>,IQueryHandler<CustomerLoginCommand, AuthTokenResponseModel>
    {
        public async Task<ResponseModel<AuthTokenResponseModel>> Handle(CustomerLoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var email = command.EmailAddress;
                var password = command.Password;
                var customer = await customerQueryRepository.GetByEmail(email);
                if (customer is null)
                    return Fail(ErrorType.NotCorrectEmailOrPassword);
                if(customer.StatusId == (int)Enums.CustomerStatusId.TemporaryDeleted)
                    return Fail(ErrorType.Customer.AccountIsTemporaryDeleted);
                var hasher = new PasswordHasher<CustomerEntity>();
                var passComparison = hasher.VerifyHashedPassword(customer, customer.HashPassWord, password);
                if (passComparison != PasswordVerificationResult.Success)
                    return Fail(ErrorType.NotCorrectEmailOrPassword);
                return authService.CustomerAuth(new CustomerAuthRequestModel
                {
                    EmailAdress = email,
                    Id = customer.Id
                });
            }
            catch (Exception ex)
            {
                return Fail(ex.Message);
            }
        }
    }
}
