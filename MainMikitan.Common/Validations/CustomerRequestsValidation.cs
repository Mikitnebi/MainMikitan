using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.InternalServiceAdapter.Validations
{
    public class CustomerRequestsValidation
    {
        public static ResponseModel<bool> Registration(CustomerRegistrationRequest model)
        {
            var response = new ResponseModel<bool>();
            if (model.RequiredOptions == false && (model.MobileNumber == null || model.MobileNumber.Length != 9 || model.MobileNumber[0] != '5'))
            {
                response.ErrorType = ErrorType.NotCorrectMobileNumberType;
                return response;
            }
            if (model.RequiredOptions && (model.Email == null || model.Email.Length < 5))
            {
                response.ErrorType = ErrorType.NoTcorrectEmailAddressType;
                return response;
            }
            if(!model.AdultnessConfirmation)
            {
                response.ErrorType = ErrorType.UserIsNotAdult;
                return response;
            }
            var passwordCheckResult = PasswordCheck(model.Password);
            if((short)passwordCheckResult < 3)
            {
                response.ErrorType = ErrorType.NotCorrectPasswordType;
                response.ErrorMessage = passwordCheckResult.ToString();
                return response;
            }
            response.Result = true;
            return response;
        }
        private static Enums.PasswordScore PasswordCheck(string password)
        {
            short score = 0;
            if (password.Length < 1)
                return Enums.PasswordScore.Blank;
            if (password.Length < 4)
                return Enums.PasswordScore.VeryWeak;
            score++;
            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"[0-9]", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success &&
              Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
                score++;

            return (Enums.PasswordScore)score;
        }
    }
}
