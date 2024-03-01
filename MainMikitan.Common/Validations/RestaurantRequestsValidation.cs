using FluentEmail.Core.Models;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.InternalServicesAdapter.Validations {
    public class RestaurantRequestsValidation {
        public static ResponseModel<bool> RegistrationIntro(RestaurantRegistrationIntroRequest model) {
            var response = new ResponseModel<bool>();
            if (model.PhoneNumber == null || model.PhoneNumber.Length != 9 || model.PhoneNumber[0] != '5') {
                response.ErrorType = ErrorResponseType.InCorrectMobileNumberType;
                return response;
            }
            if (model.RegionId > Enum.GetValues(typeof(RegionId)).Length || model.RegionId < Enum.GetValues(typeof(RegionId)).Length) {
                response.ErrorType = ErrorResponseType.NotCorrectRegionId;
                return response;
            }
            if (model.BusinessNameGeo == null || model.BusinessNameEng == null) {
                response.ErrorType = ErrorResponseType.NotAppropriateBusinessName;
                return response;
            }
            response.Result = true;
            return response;                  
        }
        public static ResponseModel<bool> RegistrationFinal(RestaurantInfoRequest model) {
            return null;
        }
    }
}
