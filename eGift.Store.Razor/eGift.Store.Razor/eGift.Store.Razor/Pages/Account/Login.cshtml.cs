using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eGift.Store.Razor.Pages.Account
{
    public class LoginModel : PageModel
    {
        #region Properties

        [BindProperty]
        public SignInViewModel SignInViewModel { get; set; } = default!;

        #endregion

        #region Constructors

        public LoginModel()
        {
        }

        #endregion

        #region Login Default Get/Post Actions

        public IActionResult OnGet()
        {
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ToastrViewModel tosterModel = null;
            try
            {
                // Server side validation
                if (ModelState.IsValid)
                {
                    // Web client api call
                    string response = WebAPIHelper.GetWebAPIClient($"Login/CusotmerLogin?userName={SignInViewModel.UserName}&password={SignInViewModel.Password}").Result;
                    if (!string.IsNullOrEmpty(response))
                    {
                        var loginModel = JsonConvert.DeserializeObject<LoginViewModel>(response);
                        if (loginModel != null)
                        {
                            if (loginModel.IsActive)
                            {
                                // Set user sessions
                                HttpContext.Session.SetInt32("UserID", loginModel.RefId);
                                HttpContext.Session.SetString("UserName", loginModel.UserName);

                                // Last Login
                                DateTime loginDateTime = DateTime.Now;

                                loginModel.LogInDate = loginDateTime;

                                // Serialize model to json string
                                var modelData = JsonConvert.SerializeObject(loginModel);

                                // Web client api call
                                string lastLoginResponse = WebAPIHelper.PostWebAPIClient($"Login/UpdateLastLogin", modelData).Result;
                                if (!string.IsNullOrEmpty(lastLoginResponse))
                                {
                                    var lastLoginModel = JsonConvert.DeserializeObject<LoginViewModel>(lastLoginResponse);

                                    // Set last login sessions
                                    HttpContext.Session.SetString("LoginDateTime", lastLoginModel.LogInDate.ToDateTimeString());
                                    HttpContext.Session.SetString("LastLogin", lastLoginModel.LastLogInDate == null ? lastLoginModel.LogInDate.ToDateTimeString() : lastLoginModel.LastLogInDate.ToDateTimeString());
                                }

                                tosterModel = new ToastrViewModel()
                                {
                                    Type = (int)ToastrType.Success,
                                    Message = ToastrMessages.LoginSuccess.GetEnumDescription<ToastrMessages>()
                                };

                                // Serialize the model to JSON before storing it in TempData
                                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                                return RedirectToPage("/Index");
                            }
                            else
                            {
                                tosterModel = new ToastrViewModel()
                                {
                                    Type = (int)ToastrType.Warning,
                                    Message = ToastrMessages.LoginInactive.GetEnumDescription<ToastrMessages>()
                                };

                                // Serialize the model to JSON before storing it in TempData
                                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                                return Page();
                            }
                        }
                    }
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Warning,
                        Message = ToastrMessages.LoginInvalid.GetEnumDescription<ToastrMessages>()
                    };

                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                }
                else
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Error,
                        Message = ToastrMessages.LoginError.GetEnumDescription<ToastrMessages>()
                    };

                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                }
            }
            catch (Exception ex)
            {
                tosterModel = new ToastrViewModel()
                {
                    Type = (int)ToastrType.Error,
                    Message = ToastrMessages.LoginError.GetEnumDescription<ToastrMessages>()
                };

                // Serialize the model to JSON before storing it in TempData
                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            }
            return Page();
        }

        #endregion

        #region Logout Actions

        public IActionResult OnGetLogout()
        {
            // Reset all session
            HttpContext.Session.Clear();

            // Clear all cart items
            TempCartData.Items.Clear();

            // Toastr Message
            var tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Success,
                Message = ToastrMessages.LogoutSuccess.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return RedirectToPage("/Index");
        }

        #endregion
    }
}
