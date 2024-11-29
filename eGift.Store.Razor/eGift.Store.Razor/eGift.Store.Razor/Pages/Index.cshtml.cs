using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models;
using eGift.Store.Razor.Models.ListViewModels;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace eGift.Store.Razor.Pages
{
    public class IndexModel : PageModel
    {
        #region Properties

        [BindProperty]
        public ProductListViewModel ProductListViewModel { get; set; }

        [BindProperty]
        public SignInViewModel SignInViewModel { get; set; }

        #endregion

        #region Constructors

        public IndexModel()
        {
            ProductListViewModel = new ProductListViewModel();
        }

        #endregion

        #region Index Default Get Actions

        public void OnGet()
        {
            try
            {
                // Web api call
                var response = WebAPIHelper.GetWebAPIClient("Product").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    ProductListViewModel.ProductList = JsonConvert.DeserializeObject<List<ProductViewModel>>(response);
                }
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
            }
        }

        #endregion

        #region Login Ajax Actions

        public JsonResult OnPostLogin(string userName, string password)
        {
            ToastrViewModel tosterModel = null;
            try
            {
                SignInViewModel = new SignInViewModel()
                {
                    UserName = userName,
                    Password = password
                };

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
                            return new JsonResult(true);
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
                            return new JsonResult(false);
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
                return new JsonResult(false);
            }
            catch (JsonReaderException ex)
            {
            }
            return new JsonResult(false);
        }

        public JsonResult OnPostAddToCart(int productId)
        {
            //ToastrViewModel tosterModel = null;
            try
            {
                // Web client api call
                string response = WebAPIHelper.GetWebAPIClient($"Product/{productId}").Result;
                if (!string.IsNullOrEmpty(response))
                {
                    var productModel = JsonConvert.DeserializeObject<ProductViewModel>(response);
                    if (productModel != null)
                    {
                        if (TempCartData.Items != null && TempCartData.Items.Any(x => x.ProductId == productId))
                        {
                            var existingProduct = TempCartData.Items.FirstOrDefault(x => x.ProductId == productId);
                            if (existingProduct != null)
                            {
                                existingProduct.Quantity += 1;
                                existingProduct.NetAmount = (existingProduct.UnitPrice - (productModel.UnitPrice * (productModel.Discount ?? 0) / 100)) * existingProduct.Quantity;
                            }
                        }
                        else
                        {
                            if (TempCartData.Items == null)
                            {
                                TempCartData.Items = new List<OrderDetailsViewModel>();
                            }
                            var orderDetailModel = new OrderDetailsViewModel()
                            {
                                ProductId = productModel.ID,
                                ProductName = productModel.Name,
                                Quantity = 1,
                                UnitPrice = productModel.UnitPrice,
                                Discount = productModel.Discount,

                                //Tax = existingProduct.Tax,
                                NetAmount = productModel.UnitPrice - (productModel.UnitPrice * (productModel.Discount ?? 0) / 100)
                            };
                            TempCartData.Items.Add(orderDetailModel);
                        }

                        //tosterModel = new ToastrViewModel()
                        //{
                        //    Type = (int)ToastrType.Success,
                        //    Message = ToastrMessages.ProductAddToCartSuccess.GetEnumDescription<ToastrMessages>()
                        //};

                        //// Serialize the model to JSON before storing it in TempData
                        //TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
                        return new JsonResult(true);
                    }
                }
            }
            catch (Exception)
            {
            }

            //tosterModel = new ToastrViewModel()
            //{
            //    Type = (int)ToastrType.Error,
            //    Message = ToastrMessages.ProductAddToCartError.GetEnumDescription<ToastrMessages>()
            //};

            //// Serialize the model to JSON before storing it in TempData
            //TempData["ToastrModel"] = JsonConvert.SerializeObject(tosterModel);
            return new JsonResult(false);
        }

        #endregion
    }
}