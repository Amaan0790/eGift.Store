using System.ComponentModel.DataAnnotations;

namespace eGift.Store.Razor.Models.ViewModels
{
    public class LoginViewModel
    {
        #region Constructors

        public LoginViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Ref")]
        [Required]
        public int RefId { get; set; }

        [Display(Name = "Ref Type")]
        [Required]
        public string RefType { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Role")]
        [Required]
        public int RoleId { get; set; }

        [Display(Name = "Active")]
        [Required]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Login Date")]
        public DateTime? LogInDate { get; set; }

        [Display(Name = "Last Login Date")]
        public DateTime? LastLogInDate { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

        #endregion View Models
    }
}