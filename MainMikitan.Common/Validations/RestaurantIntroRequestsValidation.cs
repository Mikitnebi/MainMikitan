using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Requests.RestaurantRequests;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.InternalServiceAdapter.Validations {
    public class RestaurantIntroRequestsValidation {
        public static ResponseModel<bool> Registration(RestaurantRegistrationIntroRequest model) {
            var response = new ResponseModel<bool>();
            if (model.PhoneNumber == null || model.PhoneNumber.Length != 9 || model.PhoneNumber[0] != '5') {
                response.ErrorType = ErrorType.NotCorrectMobileNumberType;
                return response;
            }
            if (model.RegionId > Enum.GetValues(typeof(RegionId)).Length || model.RegionId < Enum.GetValues(typeof(RegionId)).Length) {
                response.ErrorType = ErrorType.NotCorrectRegionId;
                return response;
            }
            if (model.BusinessName == null) {
                response.ErrorType = ErrorType.NotAppropriateBusinessName;
                return response;
            }
            response.Result = true;
            return response;
        }
    }
}
