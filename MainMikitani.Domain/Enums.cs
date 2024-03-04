using MainMikitan.Domain.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain
{
    public abstract class Enums
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
            Manager = 2,
            Admin = 3,
            Staff = 4
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
            OnlyMobileVerified = 5,
            TemporaryDeleted = 6,
            Deleted = 7
        }
        public enum UserTypeId
        {
            CustomerIntro = 0,
            Customer = 1,
            LegalEntityIntro = 2,
            LegalEntity = 3,
            RestaurantIntro = 4, 
            Restaurant = 5
        }

        public enum OtpOperationTypeId
        {
            CustomerDeleteAccount = 0,
            CustomerPasswordReset = 1,
            CustomerRegistration = 2,
            RestaurantIntroRegistration = 3 ,
            LegalEntityIntroRegistration = 4
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

        public enum LegalEntityStatusId
        {
            NonVerified = 0,
            Verified = 1
        }

        public enum LegalEntityOtpVerificationId { 
            NonVerified = 0,
            Verified = 1
        }

        public enum BusinessType {
            Cafe,
            Bar,
            CafeBar,
            Restaurant,
            NightClub,
            Club,
            Pub,
            SushiBar,
            LoungeBar,
            BurgerBar,
            Pizzeria,
            FastFood
        }
        public enum RestaurantActiveStatus {
            Active,
            Inactive,
            TemporaryClosed
        } 
        public enum EmailType {
            #region Customer
            
            CustomerRegistrationEmail = 1,
            CustomerPasswordResetEmail = 2,
            CustomerAccountDelete = 3,
            
            #endregion
                
            
            #region Restaurant
            
            RestaurantRegistrationEmail = 6,
            ManagerGenerateAccount = 7,
            RestaurantPasswordReset = 8,
            RestaurantRemindUserName = 9,
            RestaurantStaffChange = 10,
            RestaurantInfoChange = 11,
            
            #endregion
            
            #region LegalEntity

            LegalEntityRegistrationEmail = 12
            
            #endregion
            

        }

        public enum RestaurantPermissionId { 
            Info = 1,
            Event = 2
        }

        public enum ListOfValueType
        {
            Kitchen = 1,
            Music = 2,
            Environment = 3,
            Citizenship = 4,
            Ingredients = 5,
            Region = 6
        }
        //Required to change these each time its Enum's size changes BY HAND
        
        public const int BusinessTypeSize = 11;

        
        //

        
    }
    public record struct ErrorResponseType
    {
        public record struct Dictionary
        {
            public const string NotFoundBySectorId = "DICTIONARY_ERROR_FOUND_BY_SECTOR_ID";
        }
        public record struct S3
        {
            public const string BucketAlreadyExisted = "S3_BUCKET_ALREADY_EXISTED";
            public const string FileIsEmpty = "S3_FILE_IS_EMPTY";
            public const string FileSizeIsMore200Kb = "S3_FILESIZE_IS_MORE_200KB";
            public const string FileTypeIsNotImage = "S3_FILETYPE_IS_ERROR_IMAGE";
            public const string ImageNotCreatedOrUpdated = "S3_IMAGE_ERROR_CREATED_OR_UPDATED";
            public const string ImageNotFound = "S3_IMAGE_ERROR_FOUND";
            public const string ImagesNotFound = "S3_IMAGES_ERROR_FOUND";
            public const string UnexpectedException = "S3_UNEXPECTED_EXCEPTION";
        }
        public record struct RestaurantIntro
        {
            public const string RestaurantNotUpdated = "RESTAURANTINTRO_RESTAURANT_ERROR_UPDATED";
            public const string VerifiedRestaurantNotFound = "RESTAURANTINTRO_VERIFIED_RESTAURANT_ERROR_FOUND";
            public const string RestaurantWithThisMailAlreadyExisted = "RESTAURANT_WITH_THIS_MAIL_ALREADY_EXISTED";
            public const string RestaurantNotAdded = "COULD_NOT_REGISTER_RESTAURANT_INTRO";
        }
        public record struct Restaurant
        {
            public const string NotUpdated = "RESTAURANT_ERROR_UPDATED";
            public const string EmailTypeNotFound = "RESTAURANT_EMAILTYPE_ERROR_FOUND";
        }

        public record struct Staff
        {
            public const string NotAdded = "STAFF_ERROR_ADDED";
            public const string IncorrectEmailOrPassword = "INCORRECT_EMAIL_OR_PASSWORD";
            public const string StaffForbiddenPermission = "STAFF_FORBIDDEN_PERMISSION";
        }

        public record struct RestaurantEnvironment {
            public const string ErrorDelete = "RESTAURANTENVIRONMENTINFOdb_ERROR_DELETED";
            public const string ErrorAdd = "RESTAURANTENVIRONMENTINFOdb_ERROR_ADDED";
            public const string ErrorDbSave = "RESTAURANTENVIRONMENTINFOdb_ERROR_SAVECHANGED";
        }

        public record struct CustomerInfo
        {
            public const string NotCreated = "CUSTOMERINFOdb_ERROR_CREATED";
            public const string NotDbSave = "CUSTOMERINFOdb_ERROR_SAVECHANGED";
            public const string NotGetInfo = "CUSTOMERINFOdb_ERROR_GET_INFO";
        }

        public record struct CustomerInterest
        {
            public const string NotDelete = "CUSTOMERINTERESTdb_ERROR_DELETE";
            public const string NotAdd = "CUSTOMERINTERESTdb_ERROR_ADD";
            public const string NotDbSave = "CUSTOMERINTERESTEDdb_ERROR_SAVECHANGED";
        }

        public record struct Customer
        {
            public const string NotUpdated = "CUSTOMER_ERROR_UPDATED";
            public const string IsDeleted = "CUSTOMER_IS_DELETED";
            public const string IsNotDeleted = "CUSTOMER_IS_ERROR_DELETED";
            public const string NotDeleted = "CUSTOMER_ERROR_DELETED";
            public const string AccountIsTemporaryDeleted = "CUSTOMER_IS_TEMPORARY_DELETED";
            public const string NotFound = "CUSTOMER_ERROR_FOUND";
        }

        public record struct Otp
        {
            public const string IncorrectEmail = "OTP_INCORRECT_EMAIL";
            public const string AlreadyUsedOtp = "OTP_ALREADY_USED";
            public const string NotValidOtp = "OTP_ERROR_VALID";
            public const string NotCorrectOtp = "OTP_ERROR_CORRECT";
            public const string OtpNotUpdated = "OTP_ERROR_UPDATED";
        }

        public record struct OtpLog
        {
            public const string OtpLogNotCreated = "OTP_LOG_ERROR_CREATED";
        }

        public record struct EmailSender
        {
            public const string EmailNotSend = "EMAIL_ERROR_SEND";
        }

        public record struct Reservation
        {
            public const string CustomerHasReservation = "CUSTOMER_HAS_RESERVATION";
        }
        public record struct RestaurantInfo
        {
            public const string ErrorUpdate = "RESTAURANT_INFO_ERROR_UPDATE";
            public const string ErrorSaveChanges = "RESTAURANT_INFO_ERROR_SAVE_CHANGES";
            public const string ErrorCreate = "RESTAURANT_INFO_ERROR_CREATE";
            public const string ErrorRestaurantNotFound = "RESTAURANT_BY_ID_NOT_FOUND";
        }
        
        public record struct EventDetails
        {
            public const string ErrorEventNotFound = "EVENT_NOT_FOUND_BY_ID";
            public const string ErrorEventNameAndDescription = "INCORRECT_EVENT_NAME_OR_DESCRIPTION";
            public const string ErrorEventAddress = "EVENT_ADDRESS_NEEDED";
            public const string ErrorMusiciansNotProvided = "EVENT_MUSICIANS_NOT_PROVIDED";
            public const string ErrorPresenterNotProvided = "EVENT_PRESENTER_NOT_PROVIDED";
            public const string ErrorDancerNotProvided = "EVENT_DANCER_NOT_PROVIDED";
            public const string ErrorCreate = "EVENT_DETAILS_ERROR_CREATE";
        }
        
        public record struct Event
        {
            public const string ErrorCreate = "EVENT_ERROR_CREATE";
        }
        
        public record struct Comment
        {
            public const string CommentSaveFail = "COMMENT_SAVE_ERROR";
        }
        
        public record struct Rating
        {
            public const string RatingSaveFail = "RATING_SAVE_ERROR";
        }

        public record struct LegalEntity {
            public const string LegalEntityAlreadyExists = "SUCH_LEGAL_ENTITY_ALREADY_EXISTS";
            public const string CouldNotCreateLegalEntity = "COULD_NOT_REGISTER_LEGAL_ENTITY_INTO";
        }
        
        public const string CustomerCategoryNotUpdated = "CUSTOMER_CATEGORY_ERROR_UPDATED";

        public const string InCorrectMobileNumberType = "INCORRECT_MOBILE_NUMBER_TYPE";
        public const string NotCorrectPasswordType = "ERROR_CORRECT_PASSWORD_TYPE";
        public const string NotCorrectGenderIdType = "ERROR_CORRECT_GENDERID_TYPE";
        public const string UserIsNotAdult = "USER_IS_ERROR_ADULT";

            //Restaurants
        public const string NotCorrectRegionId = "ERROR_CORRECT_REGION_ID";
        public const string NotAppropriateBusinessName = "ERROR_APPROPRIATE_BUSINESS_NAME";

        public const string UnExpectedException = "EXCEPTION";
        public const string AlreadyUsedEmail = "ALREADY_USED_EMAILADDRESS";
        public const string AlreadyUsedMobileNumber = "ALREADY_USED_MOBILENUMBER";

        

        public const string NotCorrectEmailOrPassword = "ERROR_CORRECT_EMAIL_OR_PASSWORD";
        public const string NotCorrectEmailAddressType = "ERROR_CORRECT_EMAILADDRESS_TYPE";

        public const string BadTypeEmailAddress = "BAD_TYPE_EMAILADDRESS";
        public const string BadTypeFullName = "BAD_TYPE_FULLNAME";
        public const string BadCategoryIdRequest = "BAD_CATEGORYID_REQUEST";


        //RestaurantFinalRequestValidations
        public const string MoreThanMaxSizeInput = "TOO_LONG_INPUT";
        public const string NotEnoughInput = "TOO_SHORT_INPUT";
        public const string WrongBusinessTypeId = "WRONG_BUSINESS_TYPE_ID";
        public const string WrongRestaurantActiveStatusId = "WRONG_RESTAURANT_ACTIVE_STATUS_ID";
        public const string WrongHour = "WRONG_HOUR";
        public const string WrongMinute = "WRONG_MINUTE";


    }
    

    

}
