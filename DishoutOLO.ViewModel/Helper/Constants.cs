using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishoutOLO.ViewModel.Helper
{
    public class Constants
    {
        /// <summary>
        /// Date Format
        /// </summary>
        public const string DateFormat = "MM/dd/yyyy";

        /// <summary>
        /// Constant for api for invaliad user
        /// </summary>
        public const string InvalidUser = "Invalid username or password.";

        /// <summary>
        /// Constant for api for invaliad password
        /// </summary>
        public const string InvalidPassword = "Invalid password.";

        /// <summary>
        /// Login attempts
        /// </summary>
        public const string LoginAttempts = " You can try {0} more time.";

        /// <summary>
        /// Constant for locked account
        /// </summary>
        public const string AccountLocked = "You account thas been locked. Please contact admin.";

        /// <summary>
        /// Save Error
        /// </summary>
        public const string SaveError = "Error saving data to database. Please see application log table for more information.";

        /// <summary>
        /// Get Detail Error
        /// </summary>
        public const string GetDetailError = "Error retrieving data from database. Please see application log table for more information.";
        /// <summary>
        /// Detail not found
        /// </summary>
        public const string NotFound = "{0} details not found.";

        ///<summary>
        /// Added Successfully
        /// </summary>
        public const string AddedSuccessfully = "{0} added successfully";

        ///<summary>
        /// Login Success
        /// </summary>
        public const string LoginSuccess = "Login successfully";

        ///<summary>
        /// Updated Successfully
        /// </summary>
        public const string UpdatedSuccessfully = "{0} Updated successfully";

        ///<summary>
        ///Test Drive Exist
        /// </summary>
        public const string TestDriveExist = "Test drive already exist for same enquiry with same date and time.";

        ///<summary>
        ///Unauthorized
        /// </summary>
        public const string Unauthorized = "Unauthorized.";

        ///<summary>
        /// Reschedule Successfully
        /// </summary>
        public const string RescheduleSuccessfully = "Test drive is reschedule successfully";

        ///<summary>
        /// Updated Successfully
        /// </summary>
        public const string DeletedSuccessfully = "{0} Deleted successfully";

        ///<summary>
        /// Logout Success
        /// </summary>
        public const string LogoutSuccess = "Logout successfully";

        ///<summary>
        /// PastDateNotAllowed
        /// </summary>
        public const string PastDateNotAllowed = "Please select future Next Follow Up Date";
    }
    //public class PathConstant
    //{
    //    #region Developemnt Enviroment Path
    //    public const string LM_APPLICATION_PHYSICAL_PATH = @"D:/Projects/Extra Project/Phonix Tech/lead-managment/Leadmanagement/BrochureData";
    //    #endregion

    //    #region Production Enviroment paths

    //    #endregion

    //}

}
