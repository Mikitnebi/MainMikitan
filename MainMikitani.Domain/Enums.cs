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
        public enum CustomerStatusId
        {
            NoneVerified = 0,
            Verified = 1,
            Blocked = 2,
            Paused = 3
        }
        public enum GenderId
        {
            Female = 0,
            Male = 1
        }
        public enum RegionId { 
            Tbilisi = 1
        }
    }
    public class ErrorType
    {
        //Customers
        public static readonly string NotCorrectMobileNumberType = "NOT_CORRECT_MODILE_NUMBER_TYPE";
        public static readonly string NotCorrectPasswordType = "NOT_CORRECT_PASSWORD_TYPE";
        public static readonly string NotCorrectGenderIdType = "NOT_CORRECT_GENDERID_TYPE";

        public static readonly string UserIsNotAdult = "USER_IS_NOT_ADULT";
        public static readonly string UnExpectedException = "Exception";
        public static readonly string AlreadyUsedEmail = "Already_Used_Email";

        //Restaurants


    }
    public class EmailType {
        public static readonly int CustomerRegistrationEmail = 1;
        public static readonly int customerPasswordResetEmail = 2;
        public static readonly int CustomerAccountClose = 3;
        public static readonly int CustomerAccountUpdate = 4;
    }
}
