using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.Store.Razor.Models.ViewModels
{
    public class CustomerViewModel : BaseModel
    {
        #region Constructors

        public CustomerViewModel()
        {
            GenderList = EnumHelper.EnumNameToSelectList<Gender>();
            AddressList = new SelectList("");
            LoginModel = new LoginViewModel();
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "This field is required.")]
        public int GenderId { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "This field is required.")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Address")]
        public int? AddressId { get; set; }

        [Display(Name = "Active")]
        [Required(ErrorMessage = "This field is required.")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Profile Image Path")]
        public string? ProfileImagePath { get; set; }

        [Display(Name = "Profile Image Data")]
        public byte[]? ProfileImageData { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "This field is required.")]
        public int RoleId { get; set; }

        [Display(Name = "Default")]
        [Required(ErrorMessage = "This field is required.")]
        public bool IsDefault { get; set; } = false;

        #endregion Data Models

        #region View Models

        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "Address")]
        public string? AddressName { get; set; }

        [Display(Name = "Gender")]
        public string? GenderName { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Role")]
        public string? RoleName { get; set; }

        [NotMapped]
        [Display(Name = "Profile Image")]
        public IFormFile? ProfileImage { get; set; }

        [NotMapped]
        public bool IsClear { get; set; }

        #endregion View Models

        #region Reference View Model

        public LoginViewModel LoginModel { get; set; }

        #endregion

        #region Dropdown Models

        [NotMapped]
        public SelectList GenderList { get; set; }

        [NotMapped]
        public SelectList AddressList { get; set; }

        #endregion
    }
}