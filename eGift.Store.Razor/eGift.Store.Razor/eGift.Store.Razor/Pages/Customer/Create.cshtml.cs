using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eGift.Store.Razor.Models.Context;
using eGift.Store.Razor.Models.ViewModels;
using eGift.Store.Razor.Helpers;
using Newtonsoft.Json;
using eGift.Store.Razor.Models;
using eGift.Store.Razor.Common;
using System.ComponentModel.DataAnnotations;

namespace eGift.Store.Razor.Pages.Customer
{
    public class CreateModel : PageModel
    {
        #region Properties

        [BindProperty]
        public CustomerViewModel CustomerViewModel { get; set; } = default!;

        [BindProperty]
        public string CustomerID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "This field is required.")]
        [PageRemote(PageHandler = "VerifyUserName", HttpMethod = "GET", AdditionalFields = nameof(CustomerID))]
        public string UserName { get; set; }

        #endregion

        #region Constructors

        public CreateModel()
        {
        }

        #endregion

        #region Customer Default Get/Post Actions

        public IActionResult OnGet()
        {
            CustomerViewModel = new CustomerViewModel();
            GetAllAddress(CustomerViewModel);
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ToastrViewModel tosterModel = null;

            try
            {
                //Server side validation
                if (!ModelState.IsValid)
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Error,
                        Message = ToastrMessages.CustomerCreateError.GetEnumDescription<ToastrMessages>()
                    };

                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                    // Call Get All Address
                    GetAllAddress(CustomerViewModel);

                    return Page();
                }

                // For create
                if (CustomerViewModel.ID == 0)
                {
                    // Check if the file is not null and has content
                    if (CustomerViewModel.ProfileImage != null && CustomerViewModel.ProfileImage.Length > 0)
                    {
                        // Get the file name
                        CustomerViewModel.ProfileImagePath = Path.GetFileName(CustomerViewModel.ProfileImage.FileName);

                        using (var memoryStream = new MemoryStream())
                        {
                            CustomerViewModel.ProfileImage.CopyTo(memoryStream);
                            CustomerViewModel.ProfileImageData = memoryStream.ToArray(); // Convert to byte array
                        }
                    }

                    CustomerViewModel.RoleId = (int)Role.Customer;
                    CustomerViewModel.IsDeleted = false;
                    CustomerViewModel.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                    CustomerViewModel.CreatedDate = DateTime.Now;
                }

                // Model to json string
                var modelData = JsonConvert.SerializeObject(CustomerViewModel);

                // Web API call
                string response = WebAPIHelper.PostWebAPIClient("Customer", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    var customerModel = JsonConvert.DeserializeObject<CustomerViewModel>(response);

                    if (customerModel != null)
                    {
                        // Employee login model
                        var loginModel = new LoginViewModel()
                        {
                            IsActive = customerModel.IsActive,
                            RefId = customerModel.ID,
                            RefType = Role.Customer.ToString(),
                            UserName = UserName,
                            Password = CustomerViewModel.LoginModel.Password,
                            RoleId = (int)Role.Customer
                        };

                        // Model to json string
                        var loginModelData = JsonConvert.SerializeObject(loginModel);

                        // Web API call
                        string loginResponse = WebAPIHelper.PostWebAPIClient("Login", loginModelData).Result;
                        if (!string.IsNullOrEmpty(loginResponse))
                        {
                            var loginResponseModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);
                            if (loginResponseModel != null)
                            {
                                // Set user session and Login time
                                if (loginResponseModel.IsActive)
                                {
                                    // Set user sessions
                                    HttpContext.Session.SetInt32("UserID", loginResponseModel.RefId);
                                    HttpContext.Session.SetString("UserName", loginResponseModel.UserName);

                                    // Last Login
                                    DateTime loginDateTime = DateTime.Now;

                                    loginResponseModel.LogInDate = loginDateTime;

                                    // Serialize model to json string
                                    var loginResponseModelData = JsonConvert.SerializeObject(loginResponseModel);

                                    // Web client api call
                                    string lastLoginResponse = WebAPIHelper.PostWebAPIClient($"Login/UpdateLastLogin", loginResponseModelData).Result;
                                    if (!string.IsNullOrEmpty(lastLoginResponse))
                                    {
                                        var lastLoginModel = JsonConvert.DeserializeObject<LoginViewModel>(lastLoginResponse);

                                        // Set last login sessions
                                        HttpContext.Session.SetString("LoginDateTime", lastLoginModel.LogInDate.ToDateTimeString());
                                        HttpContext.Session.SetString("LastLogin", lastLoginModel.LastLogInDate == null ? lastLoginModel.LogInDate.ToDateTimeString() : lastLoginModel.LastLogInDate.ToDateTimeString());
                                    }
                                }
                            }

                            tosterModel = new ToastrViewModel()
                            {
                                Type = (int)ToastrType.Success,
                                Message = ToastrMessages.CustomerCreateSuccess.GetEnumDescription<ToastrMessages>()
                            };

                            //TempData["ToastrModel"] = tosterModel;
                            // Serialize the model to JSON before storing it in TempData
                            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                            return RedirectToPage("/Index");
                        }
                    }
                }
            }
            catch
            {
            }
            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.CustomerCreateError.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            // Call Get All Address
            GetAllAddress(CustomerViewModel);

            return Page();
        }

        #endregion

        #region Private Methods

        // Get All Addresses
        public void GetAllAddress(CustomerViewModel model)
        {
            // Web api call
            var response = WebAPIHelper.GetWebAPIClient("Address").Result;
            if (response != null)
            {
                var addressList = JsonConvert.DeserializeObject<List<AddressViewModel>>(response);

                // Create a SelectList
                model.AddressList = new SelectList(addressList, "ID", "FullAddress");
            }
        }

        #endregion

        #region Remote Validation Actions

        public IActionResult OnGetVerifyUserName(int CustomerID, string UserName)
        {
            // Web client api call
            string isExistResponse = WebAPIHelper.GetWebAPIClient($"Customer/VerifyUserName?id={CustomerID}&userName={UserName}").Result;

            if (Convert.ToBoolean(isExistResponse))
            {
                return new JsonResult($"Username {UserName} is already in use.");
            }

            return new JsonResult(true);
        }

        #endregion
    }
}
