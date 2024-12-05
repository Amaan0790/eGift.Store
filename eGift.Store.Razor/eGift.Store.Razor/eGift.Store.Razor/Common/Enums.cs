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

    public enum Status
    {
        [Description("New")]
        New = 1,

        [Description("Dispatched")]
        Dispatched = 2,

        [Description("Shipped")]
        Shipped = 3,

        [Description("Delivered")]
        Delivered = 4,

        [Description("Cancelled")]
        Cancelled = 5,

        [Description("Completed")]
        Completed = 6,
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

        [Description("Product added to cart.")]
        ProductAddToCartSuccess = 10,

        [Description("Unable to add product to cart.")]
        ProductAddToCartError = 11,

        // Place Order
        [Description("Order placed successfully.")]
        PlaceOrderSuccess = 12,

        [Description("Unable to place order.")]
        PlaceOrderError = 13,
    }

    #endregion

    #endregion
}
