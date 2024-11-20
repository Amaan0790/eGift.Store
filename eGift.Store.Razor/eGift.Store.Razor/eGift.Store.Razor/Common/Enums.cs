using System.ComponentModel;

namespace eGift.Store.Razor.Common
{
    #region General

    public enum Gender
    {
        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2
    }

    #endregion

    #region Project Specific

    public enum Role
    {
        [Description("Employee")]
        Employee = 1,

        [Description("Customer")]
        Customer = 2,

        [Description("Admin")]
        Admin = 3
    }

    #endregion

    #region Toastr

    #region Toastr Type

    public enum ToastrType
    {
        [Description("Success")]
        Success = 1,

        [Description("Info")]
        Info = 2,

        [Description("Warning")]
        Warning = 3,

        [Description("Error")]
        Error = 4
    }

    #endregion

    #region Toastr Messages

    public enum ToastrMessages
    {
        // Login
        [Description("Login successfully.")]
        LoginSuccess = 1,

        [Description("Invalid credentials.")]
        LoginInvalid = 2,

        [Description("This user is inctive.")]
        LoginInactive = 3,

        [Description("An error occured while login.")]
        LoginError = 4,

        // Logout
        [Description("Logout successfully.")]
        LogoutSuccess = 5,

        // Customer
        [Description("Customer register successfully.")]
        CustomerCreateSuccess = 6,

        [Description("Unable to register customer.")]
        CustomerCreateError = 7,

        [Description("Profile updated successfully.")]
        CustomerEditSuccess = 8,

        [Description("Unable to update profile.")]
        CustomerEditError = 9,
    }

    #endregion

    #endregion
}
