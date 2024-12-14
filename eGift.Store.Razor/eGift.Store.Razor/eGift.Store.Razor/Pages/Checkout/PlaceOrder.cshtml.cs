using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models;
using eGift.Store.Razor.Models.RequestModels;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace eGift.Store.Razor.Pages.Checkout
{
    public class PlaceOrderModel : PageModel
    {
        #region Properties

        [BindProperty]
        public OrderViewModel OrderModel { get; set; } = default!;

        [BindProperty]
        public List<OrderDetailsViewModel> OrderList { get; set; } = default!;

        #endregion

        #region Constructors

        public PlaceOrderModel()
        {
        }

        #endregion

        #region Place Order Default Get/Post Actions

        public async Task OnGetAsync()
        {
            try
            {
                int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

                // Web api call
                var response = WebAPIHelper.GetWebAPIClient($"Customer/{customerId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    var customerModel = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                    if (customerModel != null)
                    {
                        // Web api call
                        var addressResponse = WebAPIHelper.GetWebAPIClient($"Address/{customerModel.AddressId}").Result;
                        if (!string.IsNullOrEmpty(addressResponse))
                        {
                            var addressModel = JsonConvert.DeserializeObject<AddressViewModel>(addressResponse);
                            OrderModel = new OrderViewModel()
                            {
                                CustomerId = customerModel.ID,
                                OrderNumber = GenerateRandomString(10),
                                CustomerName = customerModel.FirstName + " " + customerModel.LastName,
                                MobileNumber = customerModel.Mobile,
                                Address = addressModel?.FullAddress
                            };

                            if (TempCartData.Items?.Count > 0)
                                OrderList = TempCartData.Items;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ToastrViewModel tosterModel = null;
            try
            {
                if (OrderModel != null)
                {
                    if (TempCartData.Items?.Count > 0)
                    {
                        // Order
                        OrderModel.TotalAmount = TempCartData.Items.Sum(x => x.NetAmount);
                        OrderModel.TotalDiscount = TempCartData.Items.Sum(x => x.DiscountAmount);
                        OrderModel.TotalTax = TempCartData.Items.Sum(x => x.TaxAmount);
                        OrderModel.StatusId = (int)Status.New;
                        OrderModel.IsDeleted = false;
                        OrderModel.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                        OrderModel.CreatedDate = DateTime.Now;

                        // Order Request Model
                        var orderRequest = new OrderRequestModel();

                        orderRequest.OrderModel = OrderModel;

                        // Order Details
                        orderRequest.OrderDetailList = TempCartData.Items;

                        foreach (var item in orderRequest.OrderDetailList)
                        {
                            item.IsDeleted = false;
                            item.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                            item.CreatedDate = DateTime.Now;
                        }

                        // Serialize the model
                        var placeOrderJson = JsonConvert.SerializeObject(orderRequest);

                        // Web api call
                        var placeOrderResponse = WebAPIHelper.PostWebAPIClient($"Order", placeOrderJson).Result;
                        if (!string.IsNullOrEmpty(placeOrderResponse))
                        {
                            TempCartData.Items.Clear();

                            tosterModel = new ToastrViewModel()
                            {
                                Type = (int)ToastrType.Success,
                                Message = ToastrMessages.PlaceOrderSuccess.GetEnumDescription<ToastrMessages>()
                            };

                            // Serialize the model to JSON before storing it in TempData
                            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

                            return RedirectToPage("/Index");
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            tosterModel = new ToastrViewModel()
            {
                Type = (int)ToastrType.Error,
                Message = ToastrMessages.PlaceOrderError.GetEnumDescription<ToastrMessages>()
            };

            // Serialize the model to JSON before storing it in TempData
            TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);

            return Page();
        }

        #endregion

        #region Private Methods

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        #endregion
    }
}
