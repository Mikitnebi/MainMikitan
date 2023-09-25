using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain
{
    public class Enums
    {
        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }
        public enum RoleId
        {
            Customer = 0,
            Company = 1,
            Restaurant = 2,
            Admin = 3
        }
        public enum OtpStatusId
        {
            NoneVerified = 0,
            WrongAttempt = 1,
            NotValid = 2,
            Success = 3
        }
        public enum CustomerStatusId
        {
            NoneVerified = 0,
            FullyVerified = 1,
            Blocked = 2,
            Paused = 3,
            OnlyEmailVerified = 4,
            OnlyMobileVerified = 5
        }
        public enum UserTypeId
        {
            CustomerIntro = 0,
            Customer = 1,
            CompanyIntro = 2,
            Company = 3,
            RestaurantIntro = 4, 
            Restaurant = 5
        }
        public enum GenderId
        {
            Female = 0,
            Male = 1,
            None = 2
        }
        public enum RegionId { 
            Tbilisi = 1
        }
        public enum RestaurantStatusId {
            NonVerified = 0,
            Verified = 1
        }
        public enum RestaurantOtpVerificationId {
            NonVerified = 0,
            Verified = 1
        }
    }
    public class ErrorType
    {
        //Customers
        public static readonly string NotCorrectMobileNumberType = "NOT_CORRECT_MOBILE_NUMBER_TYPE";
        public static readonly string NotCorrectPasswordType = "NOT_CORRECT_PASSWORD_TYPE";
        public static readonly string NotCorrectGenderIdType = "NOT_CORRECT_GENDERID_TYPE";
        public static readonly string UserIsNotAdult = "USER_IS_NOT_ADULT";

        //Restaurants
        public static readonly string NotCorrectRegionId = "NOT_CORRECT_REGION_ID";
        public static readonly string NotAppropriateBusinessName = "NOT_APPROPRIATE_BUSINESS_NAME";

        public static readonly string UnExpectedException = "EXCEPTION";
        public static readonly string AlreadyUsedEmail = "ALREADY_USED_EMAILADDRESS";
        public static readonly string AlreadyUsedMobileNumber = "ALREADY_USED_MOBILENUMBER";

        public static readonly string EmailNotFound = "EMAIL_NOT_FOUND";
        public static readonly string AlreadyUsedOtp = "ALREADY_USED_OTP";
        public static readonly string NotValidOtp = "NOT_VALID_OTP";
        public static readonly string NotCorrectOtp = "NOT_CORRECT_OTP";
        public static readonly string CustomerNotUpdated = "CUSTOMER_NOT_UPDATED";
        public static readonly string RestaurantNotUpdated = "RESTAURANT_NOT_UPDATED";
        public static readonly string OtpNotUpdated = "OTP_NOT_UPDATED";

        public static readonly string NotCorrectEmailOrPassword = "NOT_CORRECT_EMAIL_OR_PASSWORD";
        public static readonly string NoTcorrectEmailAddressType = "NOT_CORRECT_EMAILADDRESS_TYPE";

        public const string BadTypeEmailAddress = "BAD_TYPE_EMAILADDRESS";
        public const string BadTypeFullName = "BAD_TYPE_FULLNAME";
        public const string BadCategoryIdRequest = "BAD_CATEGORYID_REQUEST";
    }
    public class EmailType {
        //
        public static readonly int CustomerRegistrationEmail = 1;
        public static readonly int customerPasswordResetEmail = 2;
        public static readonly int CustomerAccountClose = 3;
        public static readonly int CustomerAccountUpdate = 4;
        //
        public static readonly int RestaurantRegistrationEmail = 5;

    }
}
