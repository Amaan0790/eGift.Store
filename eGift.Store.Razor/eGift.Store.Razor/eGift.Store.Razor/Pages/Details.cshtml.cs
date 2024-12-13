using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eGift.Store.Razor.Models.Context;
using eGift.Store.Razor.Models.ViewModels;
using eGift.Store.Razor.Helpers;
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
