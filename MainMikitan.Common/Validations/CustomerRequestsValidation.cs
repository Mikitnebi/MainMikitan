﻿using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Common.Validations
{
    public class CustomerRequestsValidation
    {
        public static ResponseModel<bool> Registration (CustomerRegistrationRequest model)
        {
            var response = new ResponseModel<bool> ();
            if(model.MobileNumber == null || model.MobileNumber.Length != 9 || model.MobileNumber[0] != '5')
            {
                response.ErrorType = ErrorType.NotCorrectMobileNumberType;
                return response;
            }
            if(model.BirthDate.AddYears(18) > DateTime.UtcNow)
            {
                response.ErrorType = ErrorType.UserIsNotAdult;
                return response;
            }
            if(model.GenderId != 0 || model.GenderId != 1)
            {
                response.ErrorType = ErrorType.NotCorrectGenderIdType;
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
            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
              Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                score++;

            return (Enums.PasswordScore)score;
        }
    }
}