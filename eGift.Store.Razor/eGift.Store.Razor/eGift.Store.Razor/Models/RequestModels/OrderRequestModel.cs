using eGift.Store.Razor.Models.ViewModels;

namespace eGift.Store.Razor.Models.RequestModels
{
    public class OrderRequestModel
    {
        #region Constructors

        public OrderRequestModel()
        {
            OrderModel = new OrderViewModel();
            OrderDetailList = new List<OrderDetailsViewModel>();
        }

        #endregion

        #region Reference View Models

        public OrderViewModel OrderModel { get; set; }

        #endregion

        #region Reference List View Models

        public List<OrderDetailsViewModel> OrderDetailList { get; set; }

        #endregion
    }
}
