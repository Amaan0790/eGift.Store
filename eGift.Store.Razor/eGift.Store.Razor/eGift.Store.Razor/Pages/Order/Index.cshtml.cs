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

namespace eGift.Store.Razor.Pages.Order
{
    public class IndexModel : PageModel
    {
        #region Properties

        public IList<OrderDetailsViewModel> OrderDetailList { get; set; } = default!;

        #endregion

        #region Constructors

        public IndexModel()
        {
        }

        #endregion

        #region OrderDetails Default Get Actions

        public async Task OnGetAsync()
        {
            // Customer Id
            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            // Web api call
            var response = WebAPIHelper.GetWebAPIClient($"OrderDetails/GetByCustomerId/{customerId}").Result;
            if (!string.IsNullOrEmpty(response))
            {
                OrderDetailList = JsonConvert.DeserializeObject<List<OrderDetailsViewModel>>(response);
                OrderDetailList = (List<OrderDetailsViewModel>)OrderDetailList.OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate).ToList();
            }
        }

        #endregion
    }
}
