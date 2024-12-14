using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eGift.Store.Razor.Pages
{
    public class DetailsModel : PageModel
    {
        #region Properties

        public ProductViewModel ProductViewModel { get; set; } = default!;

        #endregion

        #region Constructors

        public DetailsModel()
        {
        }

        #endregion

        #region Product Default Get Actions

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"Product/{id}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                ProductViewModel = JsonConvert.DeserializeObject<ProductViewModel>(response);
            }

            return Page();
        }

        #endregion
    }
}
