using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace eGift.Store.Razor.Pages.Customer
{
    public class EditModel : PageModel
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

        public EditModel()
        {
        }

        #endregion

        #region Customer Default Get/Post Actions

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                // Web api call
                var response = WebAPIHelper.GetWebAPIClient($"Customer/{id}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    CustomerViewModel = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                    CustomerViewModel.Age = CalculateAge(CustomerViewModel.DateOfBirth);
                    GetAllAddress(CustomerViewModel);

                    // web api call
                    var loginResponse = WebAPIHelper.GetWebAPIClient($"Login/GetCustomerLogin/{CustomerViewModel.ID}").Result;
                    if (!string.IsNullOrEmpty(loginResponse))
                    {
                        CustomerViewModel.LoginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);
                        UserName = CustomerViewModel.LoginModel.UserName;
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ToastrViewModel tosterModel = null;
            try
            {
                // Server side validation
                if (!ModelState.IsValid)
                {
                    tosterModel = new ToastrViewModel()
                    {
                        Type = (int)ToastrType.Error,
                        Message = ToastrMessages.CustomerEditError.GetEnumDescription<ToastrMessages>()
                    };

                    //TempData["ToastrModel"] = tosterModel;
                    // Serialize the model to JSON before storing it in TempData
                    TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                    // Call Get All Address
                    GetAllAddress(CustomerViewModel);

                    return Page();
                }

                //For edit
                if (CustomerViewModel.ID > 0)
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
                    else if (CustomerViewModel.IsClear)
                    {
                        CustomerViewModel.ProfileImagePath = null;
                        CustomerViewModel.ProfileImageData = null;
                    }

                    CustomerViewModel.RoleId = (int)Role.Customer;
                    CustomerViewModel.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                    CustomerViewModel.UpdatedDate = DateTime.Now;
                }

                // Serialize model to json string
                var modelData = JsonConvert.SerializeObject(CustomerViewModel);

                // Web client api call
                string response = WebAPIHelper.PutWebAPIClient($"Customer/{CustomerViewModel.ID}", modelData).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    // Web api call for login model
                    string loginResponse = WebAPIHelper.GetWebAPIClient($"Login/GetCustomerLogin/{CustomerViewModel.ID}").Result;
                    if (!string.IsNullOrEmpty(loginResponse))
                    {
                        var existingLoginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);
                        if (existingLoginModel != null)
                        {
                            existingLoginModel.IsActive = CustomerViewModel.IsActive;
                            existingLoginModel.UserName = UserName;
                            existingLoginModel.Password = CustomerViewModel.LoginModel.Password;

                            // Model to json string
                            var loginModelData = JsonConvert.SerializeObject(existingLoginModel);

                            // Web api call
                            var updateLoginResponse = WebAPIHelper.PutWebAPIClient($"Login/{existingLoginModel.ID}", loginModelData).Result;
                            if (!string.IsNullOrEmpty(updateLoginResponse))
                            {
                                var updateLoginResponseModel = JsonConvert.DeserializeObject<LoginViewModel>(updateLoginResponse);

                                // Set user session
                                HttpContext.Session.SetString("UserName", updateLoginResponseModel?.UserName);

                                tosterModel = new ToastrViewModel()
                                {
                                    Type = (int)ToastrType.Success,
                                    Message = ToastrMessages.CustomerEditSuccess.GetEnumDescription<ToastrMessages>()
                                };

                                // Serialize the model to JSON before storing it in TempData
                                TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                                return RedirectToPage("/Index");
                            }
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
                Message = ToastrMessages.CustomerEditError.GetEnumDescription<ToastrMessages>()
            };

            //TempData["ToastrModel"] = tosterModel;
            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            // Call Get All Address
            GetAllAddress(CustomerViewModel);

            return Page();
        }

        #endregion

        #region Private Methods

        // function to calculate age based on date of birth
        private int CalculateAge(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return 0;
            }
            else
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Value.Year;

                // Check if the birthday has occurred this year; if not, subtract one year from the age
                if (dateOfBirth.Value.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }

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

        // Verify User Name
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
