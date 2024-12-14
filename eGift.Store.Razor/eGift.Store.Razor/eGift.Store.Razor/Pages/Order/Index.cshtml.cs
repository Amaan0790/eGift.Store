using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
