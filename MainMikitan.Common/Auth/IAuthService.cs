using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.InternalServiceAdapter.Auth
{
    public interface IAuthService 
    {
        ResponseModel<AuthTokenResponseModel> CustomerAuth(CustomerAuthRequestModel customerAuthModel);
    }
}
