using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eGift.Store.Razor.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        #region Properties

        [BindProperty]
        public CustomerViewModel CustomerViewModel { get; set; } = default!;

        #endregion

        #region Constructors

        public DetailsModel()
        {
        }

        #endregion

        #region Customer Default Get Actions

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
                    GetFullAddress(CustomerViewModel);
                    CustomerViewModel.GenderName = ((Gender)CustomerViewModel.GenderId).ToString();
                    CustomerViewModel.RoleName = ((Role)CustomerViewModel.RoleId).ToString();
                }
            }

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

        // Get Full Address
        public void GetFullAddress(CustomerViewModel model)
        {
            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"Address/{model.AddressId}").Result;
            if (response != null)
            {
                var addressModel = JsonConvert.DeserializeObject<AddressViewModel>(response);

                // Assign AddressName
                model.AddressName = addressModel?.FullAddress;
            }
        }

        #endregion
    }
}
